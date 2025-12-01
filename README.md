# Stammbaum für Waisenkinder - Projektdokumentation

## 🎯 Projektvision

Dieses Projekt entwickelt ein System, das es Waisenkinder ermöglicht, digitale Stammbäume ihrer leiblichen Familie zu erstellen und zu verwalten. Dadurch können sie mehr über ihre Herkunft und Verwandtschaft erfahren und ihre Identität besser verstehen.

---

## 📋 Projektinformationen

| Eigenschaft | Wert |
|------------|------|
| **Projektname** | Stammbaum für Waisenkinder |
| **Repository** | 3AHIT_2025_26_Glück_Hutter_Stammbaum |
| **Teamleiter** | Hutter, Glück |
| **Klasse** | 3AHIT |
| **Projektstart** | Dezember 2025 |
| **Projektende** | 9. Januar 2026 |
| **Programmiersprache** | C# (.NET 9.0) |
| **Framework** | ASP.NET Core Blazor Server |
| **Datenbank** | SQLite |

---

## 🎯 Projektziele

### Primäre Ziele
- ✅ Benutzerregistrierung und Authentifizierung
- ✅ Digitale Verwaltung von Familienmitgliedern
- ✅ Stammbaum-Visualisierung
- ✅ Stammbäume exportierbar machen (PNG, CSV)

### Messkriterien
- **Bewertungen**: Mindestens 300 Benutzer zufrieden
- **Registrierungen**: Mindestens 500 Waisenkinder registriert
- **Datenschutz**: Keine GDPR-Verstöße
- **Kosten**: 0€ (Open Source)

---

## 🚀 Meilensteine

### Meilenstein 1: Codestruktur und Datenbank ✅ FERTIG
**Deadline**: 5. Dezember 2025  
**Status**: ✅ **ABGESCHLOSSEN**

**Umfang:**
- Projektstruktur aufgebaut (ASP.NET Core Blazor Server)
- Datenmodelle implementiert (Benutzer, Person)
- Datenbankkontext konfiguriert (SQLite)
- Authentication Service erstellt
- Validierungsregeln definiert
- Sicherheitsmechanismen implementiert

**Dokumentation:**
- 📄 `MEILENSTEIN_1.md` - Abschlussbericht
- 📄 `CODESTRUKTUR.md` - Detaillierte Code-Architektur
- 📄 `DATENBANKDESIGN.md` - Datenbankschema und Design
- 📄 `VALIDIERUNG_SICHERHEIT.md` - Validierungs- und Sicherheitsmechanismen

---

### Meilenstein 2: Registrierung und Login
**Deadline**: 12. Dezember 2025  
**Status**: ⏳ IN PROGRESS

**Geplante Komponenten:**
- [ ] Register-Seite mit Validierung
- [ ] Login-Seite mit Session-Management
- [ ] Passwort-Zurücksetzen (optional)
- [ ] Benutzer-Profil Seite
- [ ] Logout-Funktionalität

---

### Meilenstein 3: Stammbaum-Funktionalität
**Deadline**: Offenes Datum  
**Status**: ⏳ PLANNED

**Geplante Features:**
- [ ] Stammbaum-Visualisierung (Graph/Chart)
- [ ] Export zu PNG
- [ ] Export zu CSV
- [ ] Stammbaumbearbeitung

---

## 📁 Dokumentation

### Hauptdokumente
1. **`MEILENSTEIN_1.md`** - Erster Meilenstein Abschlussbericht
2. **`CODESTRUKTUR.md`** - Detaillierte Code-Architektur und Design
3. **`DATENBANKDESIGN.md`** - Datenbankschema, ERD, DDL
4. **`VALIDIERUNG_SICHERHEIT.md`** - Validierung, Input-Sanitization, Sicherheit
5. **`README.md`** (diese Datei) - Projekt-Überblick

### Zusätzliche Ressourcen
- Code-Kommentare: Alle Klassen haben XML-Doc-Kommentare
- Entity-Validierung: Data Annotations in Datenmodellen
- Test-Endpoints: `/api/test/*` für Entwicklung

---

## 🔧 Technologie-Stack

### Backend
- **Framework**: ASP.NET Core 9.0
- **Rendering**: Blazor Server
- **ORM**: Entity Framework Core
- **Datenbank**: SQLite
- **Authentifizierung**: Custom AuthService
- **Passwort-Hashing**: BCrypt.Net v4.0.2

### Frontend
- **UI-Framework**: Blazor Components (.razor)
- **Styling**: Bootstrap 5
- **JavaScript**: tree-editor.js (Custom)

### Development
- **IDE**: Visual Studio Code
- **Build-System**: MSBuild / dotnet CLI
- **Testing**: MS Unit Tests (geplant)

---

## 💾 Datenbankschema

### Entitäten

#### Benutzer
```
- Id (PK, Auto-Increment)
- Email (UNIQUE, NOT NULL)
- PasswortHash (NOT NULL, BCrypt)
- Name (NOT NULL)
- Personen (1:N Navigation)
```

#### Person
```
- Id (PK, Auto-Increment)
- BenutzerId (FK, NOT NULL, CASCADE DELETE)
- Name (NOT NULL, Regex-validiert)
- Geburtsort (NOT NULL, Regex-validiert)
- Geburtsdatum (NOT NULL, Format: TT.MM.JJJJ)
- Verwandte (NOT NULL, Komma-getrennt)
- Notizen (NULLABLE)
- Benutzer (Navigation)
```

**Beziehung**: One-to-Many (1:N)  
**Cascade Delete**: Benutzer → Personen

---

## 🔐 Sicherheit und Validierung

### Validierungsebenen
1. **Client-Validation** (UI): Sofortiges Feedback
2. **Server-Validation** (Entity Attributes): Data Annotations
3. **Database-Constraints**: Unique Indices, FK Constraints

### Sicherheitsmechanismen
- ✅ **Passwort-Hashing**: BCrypt (adaptive, salted)
- ✅ **Email-Validierung**: EmailAddress Attribute + Unique Index
- ✅ **Input-Validation**: Regex für Namen, Orte, Daten
- ✅ **SQL-Injection-Schutz**: Entity Framework Core (parametrisierte Queries)
- ✅ **Authentifizierung**: AuthService + AuthStateProvider
- ✅ **Autorisierung**: [Authorize] Attribute auf geschützten Seiten

### Validierungsregeln

| Feld | Regel | Beispiel |
|------|-------|---------|
| Email | `.+@.+` | max@example.com |
| Passwort | Min 8 Zeichen | MySecurePass123 |
| Name | Buchstaben, `-` nur | Max Mustermann |
| Geburtsort | Buchstaben, `-` nur | München |
| Geburtsdatum | Zahlen, Punkte | 15.03.1980 |
| Verwandte | Buchstaben, Kommas | Marie Müller, Hans Schmidt |

---

## 🚀 Getting Started

### Voraussetzungen
- .NET 9.0 SDK
- Visual Studio Code oder Visual Studio 2024
- SQLite (automatisch mit Projekt)

### Installation
```bash
# Repository klonen
git clone https://github.com/Jakob4423/3AHIT_2025_26_Glueck_Hutter_Stammbaum.git
cd 3AHIT_2025_26_Glueck_Hutter_Stammbaum

# Dependencies installieren
dotnet restore

# Projekt bauen
dotnet build

# Anwendung starten
cd Waisenkinder
dotnet run

# Browser öffnen
https://localhost:7121
```

### Datenbank
- **Dateiort**: `Waisenkinder/waisen.db`
- **Erstellung**: Automatisch beim ersten Start (EnsureCreated)
- **Reset**: Löschen Sie `waisen.db` und starten Sie neu

---

## 📊 Projektstruktur

```
3AHIT_2025_26_Glueck_Hutter_Stammbaum/
├── ITP2Tree.sln                     # Solution-Datei
├── MEILENSTEIN_1.md                 # Meilenstein 1 Dokumentation
├── CODESTRUKTUR.md                  # Code-Architektur
├── DATENBANKDESIGN.md               # Datenbankdesign
├── VALIDIERUNG_SICHERHEIT.md        # Sicherheitsdoku
├── README.md                        # Diese Datei
└── Waisenkinder/                    # Hauptprojekt
    ├── Program.cs                   # Startup-Konfiguration
    ├── Tree.csproj                  # Projekt-Datei
    ├── Data/                        # Datenschicht
    │   ├── AppDBContext.cs          # EF Core Kontext
    │   ├── Benutzer.cs              # Benutzer-Entity
    │   ├── Person.cs                # Person-Entity
    │   └── Data.csproj              # Datenprojekt
    ├── Services/                    # Geschäftslogik
    │   ├── AuthService.cs           # Authentifizierung
    │   └── CustomAuthStateProvider.cs # Auth-Provider
    ├── Components/                  # Blazor-Komponenten
    │   ├── Pages/                   # Seiten
    │   └── Shared/                  # Freigegebene Komponenten
    ├── wwwroot/                     # Statische Dateien
    ├── appsettings.json             # Konfiguration
    └── waisen.db                    # SQLite-Datenbank
```

---

## ✍️ Codestyle und Konventionen

### Namenskonventionen
- **Klassen**: PascalCase (z. B. `AppDBContext`)
- **Methods**: PascalCase (z. B. `SignIn()`)
- **Properties**: PascalCase (z. B. `UserId`)
- **Variablen**: camelCase (z. B. `userId`)
- **Constants**: PascalCase (z. B. `DefaultValue`)

### Dokumentation
- Alle Klassen: XML-Dokumentationskommentare
- Alle öffentlichen Methods: XML-Dokumentationskommentare
- Komplexe Logik: Inline-Kommentare
- Non-eigen Code: `// (external comment)`

### Code-Format
- Nullable Reference Types: **Aktiviert**
- Target Framework: **.NET 9.0**
- Language Version: **Latest**

---

## 🧪 Testing

### Implementierte Tests
- ❌ Unit Tests: Geplant für Meilenstein 2+
- ❌ Integration Tests: Geplant für Meilenstein 3+

### Test-Endpoints (Entwicklung)
```
POST   /api/test/create-user       # Benutzer erstellen
POST   /api/test/create-person     # Person erstellen
GET    /api/test/list-persons/{id} # Personen auflisten
```

**Hinweis:** Diese Endpoints werden vor Production entfernt

---

## 🐛 Known Issues

### Aktuelle Warnungen
```
⚠️  CS8604: Mögliches Nullverweisargument (Protected.razor:214)
⚠️  CS8602: Dereferenzierung eines möglichen Nullverweises (Protected.razor:226, 241)
```

**Status**: Minor - nicht kritisch für Funktionalität  
**Plan**: Behoben in Meilenstein 2

---

## 📝 Git-Workflow

### Branches
- `main` - Produktions-Code (stabil)
- `develop` - Entwicklungs-Branch
- `feature/*` - Feature-Branches

### Commits
- Format: `[TYPE] Beschreibung`
- Types: `[FEAT]`, `[FIX]`, `[DOCS]`, `[REFACTOR]`
- Beispiel: `[FEAT] Benutzer-Registrierung implementiert`

---

## 📞 Kontakt und Support

**Projektteam:**
- Hutter
- Glück

**Schule**: HTL Villach, 3AHIT  
**Projektbetreuer**: (Lehrer-Name)

---

## 📜 Lizenz

Dieses Projekt ist Open Source und steht unter der MIT-Lizenz.

---

## 📚 Weitere Ressourcen

### Dokumentation
- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [Blazor Server Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)

### Tools
- [Visual Studio Code](https://code.visualstudio.com/)
- [.NET SDK](https://dotnet.microsoft.com/download)

---

**Dokumentationsversion**: 1.0  
**Letztes Update**: 5. Dezember 2025  
**Status**: ✅ Meilenstein 1 abgeschlossen
