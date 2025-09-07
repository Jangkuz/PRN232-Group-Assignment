# AirWaterStore Documentation

This document is for understanding overall structure and is not qualified for a SRS or SAS

## I. Overview

### 1. User Requirements

#### 1.1. Actors

#### 1.2. Use Cases

##### a. Diagram

##### b. Descriptions

### 2. Overall Functionalities

#### 2.1. Screens Flow

#### 2.2. Screen Description

#### 2.3. Screen Authorization

### 3. System High Level Design

### 3.1 Database Design

#### a. AirWaterStore

#### b. Catalog

#### c. Basket

#### d. Discount

#### e. Orderring

#### f. Chat

### 3.2 Code Packages

### 3.3 Integration Events

#### 1. UserCreated

```mermaid
sequenceDiagram
actor User
User ->> AirWaterStore.API: CreateUser()
AirWaterStore.API ->> RabbitMQ: Publish UserCreatedEvent
RabbitMQ ->> Catalog.API: Consume UserCreatedEvent
Catalog.API ->> Catalog.API: AddCustomer()
RabbitMQ ->> Chat.API: Consume UserCreatedEvent
Chat.API ->> Chat.API: AddUser()
RabbitMQ ->> Ordering.API: Consume UserCreatedEvent
Ordering.API ->> Ordering.API: AddCustomer()
```

#### 2. GameCreated

```mermaid
sequenceDiagram
actor Admin
Admin ->> Catalog.API: CreateGame()
Catalog.API ->> RabbitMQ: Publish GameCreatedEvent
RabbitMQ ->> Ordering.API: Consume GameCreatedEvent
Ordering.API ->> Ordering.API: AddGame()
```

#### 3. GameUpdated

```mermaid
sequenceDiagram
actor Admin
alt Admin update quantity
 Admin ->> Catalog.API: UpdateQuantity()
else Order update quantity
 RabbitMQ ->> Catalog.API: Consume OrderCreatedEvent
end
Catalog.API ->> Catalog.API: UpdateGameQuantity()
Catalog.API ->> RabbitMQ: Publish GameUpdatedEvent
RabbitMQ ->> Ordering.API: Consume GameUpdatedEvent
Ordering.API ->> Ordering.API: UpdateLocalQuantity()
```

#### 4. OrderCreated

```mermaid
sequenceDiagram
actor User
User ->> FE: Checkout()
FE ->> FE: ValidateLocalGameQuantity()
alt Sufficient quantity
FE ->> Basket.API: BasketCheckout()
Basket.API ->> RabbitMQ: Publish BasketCheckoutEvent
RabbitMQ ->> Ordering.API: Consume BasketCheckoutEvent
Ordering.API ->> Ordering.API: CreateOrder()
Ordering.API ->> Ordering.API: Publish OrderCreatedDomainEvent
Ordering.API ->> Ordering.API: Consume OrderCreatedDomainEvent
Ordering.API ->> RabbitMQ: Publish OrderCreatedEvent
else Insufficient quanitity
FE ->> User: Insufficient quantity response
end
```

## II. Sequence Diagrams
