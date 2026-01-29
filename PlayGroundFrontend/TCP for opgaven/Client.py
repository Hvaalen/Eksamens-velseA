import socket

server_name = 'localhost'
server_port = 7531

client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client_socket.connect((server_name, server_port))

# Input alder
age = input("Indtast barnets alder: ")

# Send til server
client_socket.send(age.encode())

# Modtag svar
response = client_socket.recv(4096) # 4096 bytes for at være sikker på at få hele JSON listen
print("Svar fra server (JSON):", response.decode())

client_socket.close()