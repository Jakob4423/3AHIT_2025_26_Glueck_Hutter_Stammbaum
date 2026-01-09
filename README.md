# Stammbaum für Waisenkinder

## Worum geht's?

Ein Webprojekt, mit dem Waisenkinder ihre Familie und ihren Stammbaum verwalten können. Mit Login, Benutzerkonten und Familienbaum-Verwaltung.

## Was brauchst du?

- .NET 9.0 SDK
- VS Code oder Visual Studio

## Installation

```bash
git clone https://github.com/Jakob4423/3AHIT_2025_26_Glueck_Hutter_Stammbaum.git
cd 3AHIT_2025_26_Glueck_Hutter_Stammbaum

dotnet restore
dotnet build

cd Waisenkinder
dotnet run
```

Dann öffnest du: `https://localhost:7121`

## Technik

- **Sprache**: C# (.NET 9.0)
- **Frontend**: Blazor Server + Bootstrap
- **Datenbank**: SQLite
- **Authentifizierung**: Custom AuthService mit BCrypt

## Was haben wir gemacht?

 Projekt-Setup (Datenbankmodelle, Auth, Security)  
 Login & Registrierung (in Arbeit)  
 Stammbaum-Visualisierung (geplant)

## Datenbankmodelle

**Benutzer:**
- Email + Passwort (gehashed)
- Name
- Viele Personen

**Person:**
- Name, Geburtsdatum, Geburtsort
- Verwandte (Beziehungen)
- Zugehörig zu Benutzer (wird gelöscht wenn Benutzer weg)

## Validierung

Wir prüfen auf Client- und Serverseite:
- Email muss gültig sein
- Passwort min. 8 Zeichen
- Namen, Orte etc. nur Buchstaben und `-`
- Daten im Format TT.MM.JJJJ

## Ordner-Struktur

```
Waisenkinder/
├── Data/              # Datenbankmodelle
├── Services/          # Auth & Business Logic
├── Components/        # UI (Pages + Shared)
├── wwwroot/           # CSS, JS, Bootstrap
└── waisen.db          # Datenbank (wird auto erstellt)
```

## Team

- Hutter
- Glück

**Klasse:** 3AHIT, HTL Weiz  
**Projektstart:** Dezember 2025  
**Projektende:** 9. Januar 2026
