# Projet Web 2020

## Scope du projet
Mettre en place un site web public avec authentification et rôles utilisant un backend composé d’une API en .NET Core 3.1 et un frontend au choix.

Le but du site web est de permettre à des gestionnaires de bar, boîte de nuit, salle de concert ou cercle étudiant de se créer un compte sur le site et d’inscrire un ou plusieurs établissements.
Un administrateur doit valider les établissements avant qu’ils puissent apparaitre de manière publique pour un utilisateur non loggé.

## Demandes business
Les établissements seront affichés par défaut sur base d’une carte.

Les utilisateurs authentifiés visualiseront sur la carte uniquement les établissements ouverts le jour-même, un indicateur affichera clairement le statut d’ouverture en fonction des horaires. Une fois l’horaire dépassé, l’établissement disparait de la carte. Rafraichir automatiquement la carte toutes les 15 minutes.

Les utilisateurs non authentifiés visualiseront sur la carte TOUS les établissements inscrits.

Les news seront accessibles pour tout le monde, avec une animation au choix. Les utilisateurs peuvent publier ces news sur Instagram, Facebook, Twitter ou LinkedIn.

## Contraintes techniques
- Backend : ASP.NET Core Web API.
- Frontend : ASP.NET Core MVC.
- Authentication : ASP.NET Core Identity.
- Authorization : IdentityServer4.
- La validation du numéro de TVA doit être faite via une API externe (vatlayer).
- La validation du numéro de téléphone doit être faite via libphonenumber-csharp.
- La carte doit être créee via une API externe (Google Map Charts).
- Intégration d’un date picker pour les dates.
- Utilisation de notifications type Toastr pour l’opération de sauvegarde afin d’afficher un message à l’utilisateur final.
- Validation des modèles.
- Pagination.
- Usage de scripts.
- Usage d'appels AJAX.

---

# Démarrer l'application

## Migrations
Dans le Package Manager Console :

    Update-Database -Project DrinkFinder.Infrastructure -Context DrinkFinderDomainContext
    Update-Database -Project DrinkFinder.AuthServer -Context DrinkFinderIdentityContext
    Update-Database -Project DrinkFinder.AuthServer -Context PersistedGrantDbContext
    Update-Database -Project DrinkFinder.AuthServer -Context ConfigurationDbContext
    
## Startup projects
![alt text](https://i.imgur.com/Dg5ITwS.png "Startup projects")

## Utilisateurs

| Account | Password   | Role    | ID                                   |
| ------- | ---------- | ------- | ------------------------------------ |
| admin   | `Pass123$` | Admin   | 53bb86b4-78dc-4227-b0c3-41468094aab0 |
| tony    | `Pass123$` | Manager | 0ec2fdc1-b2dd-4f8a-801f-ad2dc34bd746 |
| alice   | `Pass123$` | Manager | 9c59fbb6-c669-447e-9b2b-0a64d2a5f8f6 |
| john    | `Pass123$` | Manager | e5a49b11-1b6d-441b-ab1d-d9349c93c9e4 |
| jane    | `Pass123$` | Manager | 19c567cc-4b13-4904-b3ba-11303d02b019 |
| bob     | `Pass123$` | Member  | ba7c7c61-52dd-4d23-a703-a1d31702bf33 |
| charlie | `Pass123$` | Member  | 497991f4-da7f-45ae-b885-04b74b6a3a90 |
