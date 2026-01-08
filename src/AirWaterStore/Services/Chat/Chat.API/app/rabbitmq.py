import pika, json, threading, os

RABBIT_HOST = os.getenv("RABBIT_HOST", "rabbitmq")
EXCHANGE = "chat_broadcast"

def start_broadcast_listener(callback):
    connection = pika.BlockingConnection(pika.ConnectionParameters(host=RABBIT_HOST))
    channel = connection.channel()
    channel.exchange_declare(exchange=EXCHANGE, exchange_type='fanout', durable=True)
    result = channel.queue_declare(queue='', exclusive=True)
    queue_name = result.method.queue
    channel.queue_bind(exchange=EXCHANGE, queue=queue_name)

    def consume():
        for method_frame, properties, body in channel.consume(queue_name, auto_ack=True):
            data = json.loads(body)
            callback(data)

    threading.Thread(target=consume, daemon=True).start()

def publish_broadcast(message: dict):
    connection = pika.BlockingConnection(pika.ConnectionParameters(host=RABBIT_HOST))
    channel = connection.channel()
    channel.exchange_declare(exchange=EXCHANGE, exchange_type='fanout', durable=True)
    channel.basic_publish(exchange=EXCHANGE, routing_key='', body=json.dumps(message))
    connection.close()
