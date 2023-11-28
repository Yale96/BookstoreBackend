# BookstoreBackend
Dit is de back end van de bookstore app.
Hieronder staat beschreven hoe de applicatie gerunt kan worden:

# Backend
1) Check de code lokaal uit.
2) Zorg dat er een verbinding is met bijvoorbeeld SQL server.
3) Run de volgende commands: "add-migration SetupDatabase" en vervolgens "update-database".
4) Daarna kun je de applicatie starten.
5) De tests kunnen vanuit het project worden gerunt.
6) **BELANGRIJK**: Roep eerst het login endpoint aan met de volgende body (dit is om ervoor te zorgen dat er een sessie wordt gemaakt waarin je andere endpoints aan mag roepen):
   ```
   {
      "username": "admin",
      "password": "password"
   }
   ```

# Sample API requests
**Post Request:**
```
{
   "title": "Test Boek",
   "description": "Dit is een boek om het endpoint te testen",
   "author": "Stacker",
   "numberOfPages": 101
}
```

**Put Request:**
Geef een bestaand Id mee als parameter. Vervolgens kun je onderstaande body gebruiken:
```
{
   "title": "Test Boek",
   "description": "Dit is een boek om het endpoint te testen",
   "author": "Stacker",
   "numberOfPages": 101
}
```

De responses zijn te zien op de Swagger page na het runnen van de applicatie.

# Runnen van het frontend project:
Belangrijk is om eerst de regel (10) "[Authorize("RequireAdminRole")]" uit te commenten in de backend, zodat de frontend, de backend aan mag roepen.

1) Check de code lokaal uit.
2) Run het command "npm rund dev"

