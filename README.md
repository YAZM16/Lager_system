# Vehicle Parts Management System

A comprehensive vehicle parts inventory management system designed for automotive businesses. The system handles manufacturer management, vehicle models, parts inventory, and shopping functionality for both administrators and regular users.

## Table of Contents

1. [Overview](#overview)
2. [Features](#features)
3. [Technical Requirements](#technical-requirements)
4. [Getting Started](#getting-started)
5. [User Stories](#user-stories)
6. [Acceptance Criteria](#acceptance-criteria)
7. [Backlog Tasks](#backlog-tasks)

## Features

### Administrator Features
- Manage manufacturers (CRUD operations)
- Manage vehicle models (CRUD operations)
- Manage parts inventory
- View system reports

### User Features
- Browse parts catalog
- Shopping cart functionality
- Order management

## Technical Requirements

| Component | Version |
|-----------|---------|
| .NET Core | Latest  |
| SQL Server | Express  |
| EF Core   | Latest  |

## Getting Started

1. Prerequisites:
   - .NET Core SDK
   - SQL Server Express
   - Visual Studio (optional)

2. Setup Database:
```bash
dotnet ef database update
```

3. Run Application:
```bash
dotnet run
```

## User Stories

### Authentication
1. As a system administrator, I want to log in securely so that I can manage vehicle parts inventory.
2. As a regular user, I want to register for an account so that I can purchase vehicle parts.
3. As a registered user, I want to log in securely so that I can access my shopping cart.

### Manufacturer Management
1. As an administrator, I want to add new manufacturers so that we can expand our product offerings.
2. As an administrator, I want to view all manufacturers so that I can track our suppliers.
3. As an administrator, I want to update manufacturer information so that our records stay current.

### Vehicle Models
1. As an administrator, I want to add new vehicle models so that we can associate parts correctly.
2. As an administrator, I want to view all vehicle models so that I can manage our catalog.
3. As an administrator, I want to update vehicle model information so that our data stays accurate.

### Parts Management
1. As an administrator, I want to add new parts so that we can maintain inventory.
2. As a user, I want to browse available parts so that I can find what I need.
3. As a user, I want to add parts to my cart so that I can prepare orders.

### Shopping Cart
1. As a user, I want to view my shopping cart so that I can review my selections.
2. As a user, I want to update quantities in my cart so that I can adjust my order.
3. As a user, I want to remove items from my cart so that I can refine my selection.

## Acceptance Criteria

### Registration Flow
```gherkin
Feature: User Registration
  Scenario: Successful registration
    Given I am on the registration page
    When I enter valid username, email, and password
    And I submit the form
    Then I should receive confirmation of successful registration
    And I should be logged in automatically
```

### Part Addition
```gherkin
Feature: Add Parts to Inventory
  Scenario: Administrator adds new part
    Given I am logged in as administrator
    When I enter part details and price
    And I submit the addition request
    Then the part should appear in the inventory
    And the system should confirm successful addition
```

### Shopping Cart Operations
```gherkin
Feature: Shopping Cart Management
  Scenario: Adding item to cart
    Given I am logged in as a regular user
    When I select a part and quantity
    And I add it to my cart
    Then the item should appear in my cart
    And the total should reflect the addition
```

## Backlog Tasks

### Priority High
1. Implement payment processing integration
2. Add inventory alerts for low-stock parts
3. Develop comprehensive reporting dashboard

### Priority Medium
1. Add part categorization system
2. Implement search functionality
3. Add product reviews/ratings

### Priority Low
1. Enhance UI with better styling
2. Add export/import functionality
3. Implement API endpoints for external integration

## Contributing
Contributions are welcome! Please submit pull requests with detailed descriptions of changes made. Ensure all tests pass before submitting.
