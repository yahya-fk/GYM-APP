# README - Gestion de Salle de Sport "GYM"

Ce projet est une application web développée en utilisant ASP.NET pour la gestion d'une salle de sport nommée "GYM". L'objectif principal de cette application est de fournir une plateforme permettant aux utilisateurs de gérer leur abonnement à la salle de sport et aux administrateurs de gérer les données des clients ainsi que les abonnements.

### Fonctionnalités Principales

1. **Authentification :** L'application offre un système d'authentification pour permettre aux utilisateurs de se connecter en tant que client ou administrateur.

2. **Gestion des Clients :** Une fonctionnalité permet aux administrateurs de gérer les données des clients, y compris leurs informations personnelles, leurs abonnements et leurs paiements.

3. **Gestion des Abonnements :** Les utilisateurs peuvent consulter les détails de leur abonnement actuel, y compris la date d'expiration, et effectuer des actions telles que le renouvellement ou la mise à jour de leur abonnement.

4. **Administration :** Les administrateurs ont accès à des fonctionnalités supplémentaires pour gérer les données des clients, suivre les abonnements et effectuer des opérations de maintenance.

### Technologies Utilisées

- ASP.NET : Framework web utilisé pour le développement de l'application.
- C# : Langage de programmation utilisé pour la logique métier et le backend.
- HTML/CSS : Langages utilisés pour la conception et la mise en page de l'interface utilisateur.
- SQL Server : Système de gestion de base de données utilisé pour stocker les données des clients et des abonnements.
- Entity Framework (EF) : ORM utilisé pour la gestion de la base de données.

### Installation et Utilisation

1. Cloner le projet depuis le référentiel Git.
2. Ouvrir le projet dans un environnement de développement compatible avec ASP.NET.
3. Installer SQL Server.
4. Modifier la ligne de connexion dans le fichier "DAL/DbContext.cs" pour configurer la connexion à la base de données.
5. Installer Entity Framework en utilisant la commande `install-package EntityFramework` dans la console du Gestionnaire de packages NuGet **installer EF tools,EF Design aussi**.
6. Utiliser la commande `update-database` dans la console du Gestionnaire de packages NuGet pour appliquer les migrations et mettre à jour la base de données.
7. Lancer l'application et accéder à l'URL appropriée dans un navigateur web.
8. Se connecter en tant qu'administrateur ou client pour accéder aux fonctionnalités respectives de l'application.

### Contributions et Améliorations

Les contributions à ce projet sont les bienvenues. Si vous souhaitez proposer des améliorations, des corrections de bogues ou des fonctionnalités supplémentaires, veuillez soumettre une demande de tirage (Pull Request) dans le référentiel Git.


Pour toute question ou assistance supplémentaire, veuillez contacter [Yahya FEKRANE](mailto:fekyah0@gmail.com).

---

*Copyright © 2024 Yahya FEKRANE - Gestion de Salle de Sport "GYM". Tous droits réservés.*
