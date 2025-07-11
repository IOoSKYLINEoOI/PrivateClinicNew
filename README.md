
# PrivateClinic

Веб‑приложение для частной клиники.  
Реализует электронную запись на приём, управление пациентами и персоналом, а также работу с медицинскими результатами.

---

## 📌 Возможности

- Регистрация и авторизация (JWT)
- Личный кабинет пользователя
- Запись к врачу
- Управление департаментами, сотрудниками и расписаниями
- Хранение результатов обследований
- Роли пользователей: пациент, врач, администратор

---

## ⚙️ Стек технологий

- **Backend:** ASP.NET Core (REST API), Entity Framework Core, PostgreSQL  
- **Frontend:** Next.js, React, TypeScript, Ant Design  
- **Аутентификация:** JWT, ASP.NET Identity  
- **Прочее:** Swagger, User Secrets, CORS, AutoMapper, паттерны Repository и Service

---

## 🚀 Запуск проекта

### Backend

```bash
cd PrivateClinicNew/Clinic.Backend/Clinic.Web
dotnet restore
dotnet ef database update
dotnet run
```

### Frontend

```bash
cd PrivateClinicNew/Clinic.Frontend/frontendweb
npm install
npm run dev
```

---

## 🧩 Архитектура

Проект разделён на слои:

- **Web** — контроллеры и API  
- **Application** — бизнес-логика  
- **Core** — доменные модели и интерфейсы  
- **Infrastructure** — доступ к базе данных, интеграции  
- **DataAccess** — контекст Entity Framework и миграции

---

## 🗂 Структура проекта

```plaintext
PrivateClinicNew/
├── Clinic.Backend/
│   ├── Clinic.Application/
│   ├── Clinic.Core/
│   ├── Clinic.DataAccess/
│   ├── Clinic.Infrastructure/
│   ├── Clinic.Web/
│   └── Clinic.sln
├── Clinic.Frontend/
│   └── frontendweb/
└── README.md
```

---

## 📌 Планы развития

Переход на модульную архитектуру, где каждый функциональный блок (Appointments, Users, Patients и др.) будет выделен в отдельный автономный модуль.  
Реализация доступна в отдельном репозитории с модульным монолитом.

---

Автор: Владислав  
Лицензия: MIT
