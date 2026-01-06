# NVM Ordering System

Acesta este un sistem de gestiune si plasare a comenzilor pentru restaurante, construit folosind ASP.NET Core MVC. Aplicatia simuleaza un flux complet de E-commerce, de la administrarea meniului pana la procesul de checkout pentru clienti.

## Caracteristici Implementate

### Dashboard Principal
- Statistici dinamice: Venit total, numar total de comenzi, restaurante si produse active.
- Interfata bazata pe carduri de acces rapid pentru administrare.

### Gestiune Meniu si Restaurante
- Administrare completa (CRUD) pentru entitatile de tip Restaurant.
- Meniu interactiv pentru produse, incluzand preturi si asocierea cu restaurantele respective.
- Notificari de tip alert la adaugarea produselor in cos.

### Sistem de Cos de Cumparaturi
- Utilizatorii pot adauga mai multe produse intr-un cos virtual stocat in sesiune.
- Datele sunt pastrate temporar folosind serializarea JSON.
- Calcularea automata a pretului total.

### Flux de Checkout si Finalizare
- Pagina dedicata pentru alegerea metodei de plata: Card sau Cash la livrare.
- Daca se alege plata cu cardul, sistemul marcheaza comanda ca fiind Platita.
- Automatizare: Generarea automata a numarului de ordine, a datei si a pretului la salvarea in baza de date.

---

## Instalare si Configurare

### 1. Prerequisites
- .NET SDK (8.0 sau mai nou)
- Entity Framework Core Tools (dotnet-ef)

### 2. Configurarea Bazei de Date (SQLite)
Deschide terminalul in folderul proiectului si executa urmatoarele comenzi pentru a genera baza de date si tabelele:

```bash
# Instalarea uneltelor de migrare
dotnet tool install --global dotnet-ef

# Crearea migrației initiale
dotnet ef migrations add InitialMigration

# Aplicarea schimbarilor pe baza de date locala
dotnet ef database update
# Curatarea si construirea proiectului
dotnet build

# Lansarea aplicatiei
dotnet run
```
## Rulare cu docker
### 1. Prerequisites
- Docker Desktop instalat si functional.

### 2. Lansarea aplicatiei
Din folderul root al proiectului se executa:

```bash
docker-compose up --build
```
