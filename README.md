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

- Backend : API .NET Core 3.1.
- Frontend : Angular, knockOut, React, MVC, Razor, Blazor.
- Authentication et authorization via IdentityServer4.
- La validation du numéro de TVA doit être faite via une API externe (vatlayer).
- La validation du numéro de téléphone doit être faite via libphonenumber-csharp.
- La carte doit être créee via une API externe (Google Map Charts).
- Intégration d’un date picker pour les dates.
- Utilisation de notifications type Toastr pour l’opération de sauvegarde afin d’afficher un message à l’utilisateur final.
- Validation des modèles.
- Pagination .
- Usage de scripts.
- Usage d'appels AJAX.
