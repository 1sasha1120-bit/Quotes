# Quotes API

Невеликий REST API для керування цитатами (створення, перегляд, редагування, видалення) на **ASP.NET Core Web API (.NET 10)**. Дані зберігаються у JSON-файлі, документація та тестування — через **Swagger UI**.

Ця інструкція описує, як запустити проєкт у **Visual Studio**.

---

## Вимоги

| Що потрібно | Версія / деталі |
|---|---|
| Операційна система | Windows 10/11 |
| Visual Studio | **2026 (v18)** — будь-яка редакція, зокрема безкоштовна Community |
| Робоче навантаження (workload) | **ASP.NET and web development** |
| .NET SDK | **.NET 10** (встановлюється разом із Visual Studio 2026) |
| Інтернет | потрібен під час першого відкриття, щоб завантажилися NuGet-пакети |

> **Важливо.** Проєкт націлений на `net10.0` і має файл рішення нового формату `Quotes.slnx`. Visual Studio 2022 офіційно працює лише до .NET 9, тому для цього проєкту потрібна саме **Visual Studio 2026**.

## Покрокова інструкція

### 1. Завантажте проєкт із GitHub

**Спосіб А — клонування у Visual Studio**

1. Запустіть Visual Studio 2026.
2. На стартовому вікні оберіть **Clone a repository** (або **Git → Clone Repository**).
3. У полі **Repository location** вставте адресу репозиторію, наприклад `https://github.com/<користувач>/<репозиторій>.git`.
4. У полі **Path** вкажіть коротку локальну папку, наприклад `C:\Projects\Quotes`, і натисніть **Clone**.
5. Після клонування у **Solution Explorer** двічі клацніть `Quotes.slnx` (якщо рішення не відкрилося автоматично, див. крок 3).

**Спосіб Б — через термінал (потрібен [Git](https://git-scm.com/downloads)):**

```
git clone https://github.com/<користувач>/<репозиторій>.git
```

### 2. Встановіть Visual Studio 2026

1. Завантажте Visual Studio 2026 Community: <https://visualstudio.microsoft.com/>
2. У **Visual Studio Installer** позначте workload **ASP.NET and web development** та натисніть **Install**.
3. Якщо Visual Studio вже встановлена — відкрийте **Visual Studio Installer → Modify** і переконайтеся, що цей workload увімкнений.

### 3. Відкрийте рішення

1. Запустіть Visual Studio.
2. На стартовому вікні оберіть **Open a project or solution** (або в меню **File → Open → Project/Solution**).
3. Вкажіть файл **`Quotes.slnx`** і натисніть **Open**.

У **Solution Explorer** (праворуч) має з'явитися рішення `Quotes` з одним проєктом `Quotes`.

### 4. Дочекайтеся відновлення NuGet-пакетів і зберіть проєкт

Visual Studio автоматично завантажує потрібні пакети:

- `Microsoft.AspNetCore.OpenApi` 10.0.12
- `Swashbuckle.AspNetCore.SwaggerGen` 10.3.0
- `Swashbuckle.AspNetCore.SwaggerUI` 10.3.0

Якщо автоматично не завантажилися: правий клік на рішенні в Solution Explorer → **Restore NuGet Packages**.

Потім виконайте **Build → Build Solution** (`Ctrl + Shift + B`). У вікні **Output** має з'явитися `Build: 1 succeeded, 0 failed`.

### 5. Перевірте стартовий проєкт і профіль запуску

- Проєкт `Quotes` має бути виділений **жирним** у Solution Explorer. Якщо ні — правий клік на ньому → **Set as Startup Project**.
- На верхній панелі інструментів поруч із зеленою кнопкою запуску є випадаючий список профілів. Оберіть один із них:

| Профіль | Адреси |
|---|---|
| **http** (обирається за замовчуванням після клонування) | `http://localhost:5112` |
| **https** | `https://localhost:7059` та `http://localhost:5112` |

Для першого запуску достатньо профілю `http` — він не потребує сертифіката.

### 6. Запустіть застосунок

- **F5** — запуск із налагоджувачем (debug).
- **Ctrl + F5** — запуск без налагоджувача.

Відкриється консольне вікно застосунку з рядками на кшталт (для профілю `http` — лише другий рядок):

```
Now listening on: https://localhost:7059
Now listening on: http://localhost:5112
Application started. Press Ctrl+C to shut down.
```

### 7. Відкрийте Swagger UI

Браузер **не відкривається автоматично** (у профілях запуску вимкнено `launchBrowser`), тому відкрийте його самостійно:

- <http://localhost:5112/swagger> (профілі `http` та `https`)
- <https://localhost:7059/swagger> (лише профіль `https`)

Адреса `/` у режимі розробки автоматично перенаправляє на `/swagger`.
