# FEUP-TDIN
Programming in C#  for Distribution and Information Technologies 2019/2020 at FEUP.

## 1. Project 1 - ChaTime

For this project, we developed an Internet Relay Chat based on remote objects architectural style with .NET Remoting. Clients use an GUI where they can register/login and chat with other users. In the index interface, it's possible to see which users are only and which users are not. Client can stablish communications with one user or with several users. In the chat interface it's possible to send files, to change the colours of each user's messages, to change the chat name and to send files.

More info of this project can found [here](https://github.com/TomasNovo/FEUP-TDIN/blob/master/docs/relatorio.pdf).

### Interfaces 

#### Register
![register](https://github.com/TomasNovo/FEUP-TDIN/blob/master/ChatApp/docs/prints/register.png)  

#### Login
![login](https://github.com/TomasNovo/FEUP-TDIN/blob/master/ChatApp/docs/prints/login.png)  

#### Index
![index](https://github.com/TomasNovo/FEUP-TDIN/blob/master/ChatApp/docs/prints/index.png)  

#### Chat
![chat](https://github.com/TomasNovo/FEUP-TDIN/blob/master/ChatApp/docs/prints/chat.png)  

## 2. Project 2 - TicketFactory

For this project, we developed an enterprise distributed system where workers submit trouble tickets in a Web App and solvers assign to themselves some tickets with the objective of solving it. Solvers can view all tickets (by id, by user or by unassigned tickets) and can self assign some of them. If the solvers have doubts while solving the ticket, they can ask questions as secondary tickets to a secondary department. After the response from a member of other department or in case of no doubts, the solver can solve the ticket by sending an email to the worker that submitted the trouble ticket.

#### Worker: Trouble Ticket Submission
![submission](https://github.com/TomasNovo/FEUP-TDIN/blob/master/TicketFactory/docs/prints/submission.png)

#### Solver: Auth
![solver1](https://github.com/TomasNovo/FEUP-TDIN/blob/master/TicketFactory/docs/prints/solver1.png)  

#### Solver: View Tickets
![solver2](https://github.com/TomasNovo/FEUP-TDIN/blob/master/TicketFactory/docs/prints/solver2.png)  

#### Worker: View Ticket Info
![ticket](https://github.com/TomasNovo/FEUP-TDIN/blob/master/TicketFactory/docs/prints/ticket.png)

#### Solver: Secondary Ticket Submission
![solver3](https://github.com/TomasNovo/FEUP-TDIN/blob/master/TicketFactory/docs/prints/solver3.png)  

#### Solver: Trouble Ticket Resolution
![solver4](https://github.com/TomasNovo/FEUP-TDIN/blob/master/TicketFactory/docs/prints/solver4.png)  

#### Department: Auth
![dep1](https://github.com/TomasNovo/FEUP-TDIN/blob/master/TicketFactory/docs/prints/dep1.png)  

#### Department: View Secondary Tickets 
![dep2](https://github.com/TomasNovo/FEUP-TDIN/blob/master/TicketFactory/docs/prints/dep2.png)  

#### Department: Secondary Ticket Resolution 
![dep3](https://github.com/TomasNovo/FEUP-TDIN/blob/master/TicketFactory/docs/prints/dep3.png)  