# PRN232-Group-Assignment

This is a **personal project/proof of concetp** to learn microservices and use new technologies

Goals:

- Develop a ecommerse web with multiple microservices backends
- Use multiple Db both SQL and NoSQL
- Microservices communicate via RabbitMQ messages
- Add monitoring and scalling using grafana/otel-lgtm stack

## _Note_

Cons (tobe fixed in the future?):

- Business rule is not tight(for learning purposes)
  - If game's price is updated, the game(s) price in cart will not updated
  - Basket's quantity validation is FE based
  - Don't have menu for adding discount, only API
- Update game haven't been sync via Rabbit MQ
- The project don't have a source of truth. Thus race condition and data lose. Have not found a solution
  - After done `docker compose up -d` for the first time. Delete ordering's and airwaterstore's db due to race condition when seeding.
    - Then rerun air water store docker compose
  - If faced with race condition when seeding database:
    - Keep the table schema
    - Delete the db record
    - Rerun docker compose

## TODOs

- [ ]

## _Test account_

- User:
  - Email: <user@gmail.com>
  - Password: 123456As!
- Staff:
  - Email: <staff@gmail.com>
  - Password: 123456As!
