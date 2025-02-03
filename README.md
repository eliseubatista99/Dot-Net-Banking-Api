# Dot-Net-Banking-Api

Hello everyone and thank you for visiting my Dot Net Banking Api Project.

This project consists of an api made with a 3 tier architecture
to satisfy the needs of a homebanking application. This 3 tier architecture is made from the following layers:

- Database tier: This handles the CRUD operations in the database, and is the only tier that can read and change the data.
- Business tier: This tier communicates with the database tier, processes the retrieved data, and apply the bussiness logic and validations
- Presentation Tier: This tier communicates with he logic tier, and does the necessary operations to respond the specific needs of the client application.
