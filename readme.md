# Workshop: Application Architecture

-- full hands on, local environment expierence.

Demonstrate monitoring an application, scaling and the pittfalls when implementing an application. 

UI will show running 'jobs': Queue, InProgress, Finished (elased time)
UI will show the scaling involved: vertical and horizontal
UI will show the resources used: # of instances, CPU usage, Memory Usage
UI will show application health


API Controller will create a job, publish an event that a job is ready for processing

# Scenario's

## Basic WebApp

Resources:

WebApp
Cosmos DB

## WebApp with event-driven design

Resources:

Load balancer
Web App
EventHubs/ServiceBus => RabbitMQ
Cosmos DB

## (containerized) Microservices with event-driven design

Resources:

Load balancer
Web App
Azure Function
EventHubs/ServiceBus => RabbitMQ
Cosmos DB