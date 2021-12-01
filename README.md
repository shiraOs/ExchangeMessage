Implementation of exchange message between microservices with queue manager:
1. 	Open the queue using docker image:

    in cmd: docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.9-management
2.	Run both of the microservices.
3.	Using the Swagger UI you can send a message from the 'Sender'.
4.	Using the Swagger UI you can read a message with the 'Receiver'.

    You can also see the message received in the url - 'https://localhost:5001/api/Receiver'
    
* Using Docker
* Using RabbitMQ
* Using Swagger
