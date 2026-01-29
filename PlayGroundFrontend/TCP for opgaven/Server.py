import socket
import threading
import json

# Data: Listen fra opgaven
playgrounds = [
    {"ID": 1, "Name": "Millpark", "MaxChildren": 10, "MinChildAge": 5},
    {"ID": 2, "Name": "Secret Playground", "MaxChildren": 12, "MinChildAge": 4},
    {"ID": 3, "Name": "Library", "MaxChildren": 8, "MinChildAge": 3}
]

def handle_client(connection_socket, addr):
    print(f"Ny forbindelse fra {addr}")
    
    try:
        # 1. Modtag alder fra klienten
        message = connection_socket.recv(1024).decode().strip()
        print(f"Modtog alder: {message}")
        
        if message.isdigit():
            age = int(message)
            
            # 2. Filtrering (Find dem hvor MinChildAge <= barnets alder)
            result_list = []
            for p in playgrounds:
                if p["MinChildAge"] <= age:
                    result_list.append(p)
            
            # 3. Konverter til JSON og send retur
            json_response = json.dumps(result_list)
            connection_socket.send(json_response.encode())
        else:
            connection_socket.send("Fejl: Send venligst et tal".encode())
            
    except Exception as e:
        print(f"Fejl: {e}")
    finally:
        connection_socket.close()

def start_server():
    server_port = 7531 
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    
    # Tillad genbrug af porten hvis serveren crasher (undgår 'Address already in use')
    server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    
    server_socket.bind(('', server_port))
    server_socket.listen(5)
    
    print(f"Serveren kører på port {server_port} og er klar...")
    
    while True:
        # Accepter ny klient
        connection_socket, addr = server_socket.accept()
        
        # Start en ny tråd for at være 'Concurrent'
        client_thread = threading.Thread(target=handle_client, args=(connection_socket, addr))
        client_thread.start()
start_server()