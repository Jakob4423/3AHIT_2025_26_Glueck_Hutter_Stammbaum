const { chromium } = require('playwright');
const { exec } = require('child_process');
const http = require('http');
const path = require('path');

const APP_DIR = path.resolve(__dirname, '..');
const BASE_URL = 'http://localhost:5059';

function waitForServer(url, timeout = 15000) {
  const start = Date.now();
  return new Promise((resolve, reject) => {
    (function check() {
      http.get(url, res => {
        resolve();
      }).on('error', () => {
        if (Date.now() - start > timeout) return reject(new Error('Timeout waiting for server'));
        setTimeout(check, 300);
      });
    })();
  });
}

function startServer() {
  return new Promise((resolve, reject) => {
    const proc = exec('dotnet run --urls http://localhost:5059', { cwd: APP_DIR });
    let resolved = false;
    proc.stdout.on('data', d => {
      const s = d.toString();
      // console.log('dotnet:', s);
      if (!resolved && s.match(/Now listening on:|Now listening on|info: Microsoft.Hosting.Lifetime/)) {
        resolved = true;
        resolve(proc);
      }
    });
    proc.stderr.on('data', d => {
      // still continue; capture errors
      // console.error('dotnet err:', d.toString());
    });

    // fallback: wait for HTTP ready even if stdout didn't show string
    waitForServer(BASE_URL, 15000).then(() => { if (!resolved) { resolved = true; resolve(proc); } }).catch(err => { /* ignore */ });

    // safety timeout
    setTimeout(() => {
      if (!resolved) {
        // try to proceed, resolve anyway
        resolved = true;
        resolve(proc);
      }
    }, 15000);
  });
}

(async () => {
  console.log('Starting server...');
  const serverProc = await startServer();
  try {
    console.log('Waiting for server HTTP...');
    await waitForServer(BASE_URL);

    const browser = await chromium.launch({ headless: true });
    const page = await browser.newPage();

    // Register
    await page.goto(`${BASE_URL}/register`);
    await page.getByLabel('E-Mail').fill('ui@test.local');
    await page.getByLabel('Name').fill('UI Test');
    await page.getByLabel('Passwort').fill('Passwort123');
    await page.getByRole('button', { name: 'Registrieren' }).click();
    await page.waitForTimeout(500);

    // Login
    await page.goto(`${BASE_URL}/login`);
    await page.getByLabel('E-Mail').fill('ui@test.local');
    await page.getByLabel('Passwort').fill('Passwort123');
    await page.getByRole('button', { name: 'Login' }).click();
    await page.waitForTimeout(800);

    // Protected: add person
    await page.goto(`${BASE_URL}/protected`);
    // If not logged in, the site shows login link — but our AuthService is in-memory per connection.
    // After login above we should be signed in in the same server session.
    await page.waitForTimeout(500);

    // Fill person form
    await page.getByLabel('Name').fill('Playwright Testperson');
    await page.getByLabel('Geburtsort').fill('Graz');
    await page.getByLabel('Geburtsdatum').fill('01.01.1990');
    await page.getByLabel('Bekannte Verwandte (Komma-getrennt)').fill('Anna, Peter');
    await page.getByLabel('Notizen').fill('Automated test');
    await page.getByRole('button', { name: 'Hinzufügen' }).click();

    // Wait for table and check content
    await page.waitForSelector('table');
    const tableText = await page.locator('table').innerText();
    if (tableText.includes('Playwright Testperson')) {
      console.log('TEST PASSED: Person visible in profile.');
      await browser.close();
      // stop server
      serverProc.kill();
      process.exit(0);
    } else {
      console.error('TEST FAILED: Person not found in profile. Table content:\n', tableText);
      await browser.close();
      serverProc.kill();
      process.exit(2);
    }

  } catch (err) {
    console.error('ERROR during UI test:', err);
    try { serverProc.kill(); } catch { }
    process.exit(1);
  }
})();
