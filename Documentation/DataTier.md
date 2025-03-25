# Data Tier

First, we need to structure our data according to the application's requirements. We begin with the fundamentals and gradually build upon them.

## Choosing the database system

There are multiple database systems to choose from, but since this project is meant to showcase my skills and enhance my portfolio, I’ve decided to go with one that is commonly required in job listings: SQL.

SQL is a relational database system where data is stored in tables that can be interconnected.

Now that we have chosen a system, the next step is to determine the necessary data and fields.

### Clients

A home banking app is designed to be used by bank clients. A user must be identified by a unique ID and have other unique data, such as:

    - ClientID: PRIMARY KEY VARCHAR (64) NOT NULL,
    - Password: VARCHAR (64) NOT NULL,
    - Name: VARCHAR(64) NOT NULL,
    - Surname: VARCHAR (64) NOT NULL,
    - BirthDate: DATE NOT NULL,
    - VATNumber: VARCHAR(30) NOT NULL,
    - PhoneNumber : VARCHAR(20) NOT NULL,
    - Email: VARCHAR (60) NOT NULL,

### Accounts

To be a client of a bank, a person must have at least one account. There are multiple account types, but for this app, we will consider only three:

Current Account: Primarily used for daily transactions, ensuring funds are readily available. This account comes with an associated card.
Savings Account: Designed for saving money. This account does not require a card.
Investment Account: Similar to a savings account, but its main purpose is to generate interest on deposited funds.
An account can be associated with one or more clients (as it can have multiple holders), but in this project we will only allow one owner per account.

Typically, an account has two balances: the available balance and the pending balance. Some payments and transfers temporarily block funds until they are processed. While blocked, the money still belongs to the client but cannot be used.

For this application, we will assume that all transfers are immediate, so we will only maintain a single balance.

Funds placed in an investment account must return to the source account once the investment period ends. The account structure will look something like this:

    - Id: VARCHAR (64) NOT NULL,
    - OwnerClientId: FOREIGN KEY VARCHAR(64) NOT NULL,
    - AccoutType: CHAR (2) NOT NULL,
    - Balance: DECIMAL (20,2) NOT NULL,
    - AccountName: VARCHAR(64) NOT NULL,
    - AccountImage VARCHAR,
    - SourceAccountId: FOREIGN KEY VARCHAR(64),
    - Duration: Integer, //Number of months for an investment account
    - Interest: DECIMAL (3,2)

### Cards

A card can come in several types:

Debit: The most common type, which uses the funds from the associated current account.
Pre-Paid: This card is not linked to any account but can be loaded with funds.
Credit: This card is linked to an account, but it holds its own balance. The amount is deducted from the associated account monthly.
Meal: Typically provided by the employer, and the operations available in the app will be limited.
Banks often offer premium cards, which come with benefits like cashbacks or discounts with business partners. For this application, we will focus on cashback as the card benefit.

The structure for the Card table will be as follows:

    - CardId: PRIMARY KEY VARCHAR(64)  NOT NULL,
    - Name: VARCHAR(64) NOT NULL,
    - RelatedAccountId: FOREIGN KEY VARCHAR (64)
    - Plastic: FOREIGN KEY VARCHAR (64) NOT NULL,// Plastic is explained in the next section
    - Balance: DECIMAL (20,2)  NOT NULL,//Only applies to credit and prepaid cards
    - PaymentDay: INT //Only applies to credit
    - RequestDate: DATE NOT NULL,
    - ActivationDate: DATE
    - ExpirationDate: DATE

### Plastics

Let’s break down the example:

The bank offers two types of cards: basic and premium.

To store these card offers, we need a table in the database. A client will have a card, which will contain its own information (balance, dates, etc.), but it will also be linked to one of the types of cards the bank offers. Additionally, each card is associated with an account.

To avoid confusion between the terms card, card account, and account, I will use the term plastic to refer to a type of card offered by the bank.

In summary, we have:

Account: Has funds (could be a savings, investment, or current account).
Card: The client's card, which can be debit, credit, or prepaid. It has its own details such as balance and dates.
Plastic: A type of card offered by the bank, which could be basic or premium and might include benefits like cashback.
The structure for the Plastic table will be as follows:

    - PlasticID. PRIMARY KEY VARCHAR(64) NOT NULL,
    - CardType: CHAR(2) NOT NULL,
    - Name: VARCHAR(64) NOT NULL,
    - Cashback: DECIMAL (5,2)
    - Commission: DECIMAL (5,2)
    - IMAGE: VARCHAR,
    - IsActive: BOOL NOT NULL,

### Transactions

Clients can make transfers and payments either directly from their accounts or using a card. These actions will generate a transaction.

It's common for fees to be applied in certain cases, such as urgent transfers or transfers to different banks.

The Transaction table will include the following fields:

    - TransactionID: VARCHAR(64) NOT NULL,
    - Date: DATE NOT NULL,
    - Description: VARCHAR(64),
    - SourceAccount: VARCHAR(64) //In case the transaction was made from an account
    - DestinationName: VARCHAR(64) NOT NULL,
    - DestinationAccount: VARCHAR(64),
    - SourceCard: VARCHAR(64), //In case the transaction was made with a card
    - Amount: DECIMAL (20,2) NOT NULL,
    - Fees: DECIMAL (10, 2) NOT NULL,
    - Urgent: BOOL NOT NULL,

### Loan Offers

Loans can indeed be complex, involving a lot of specific logic, but for the sake of this project, we will simplify them without losing their core functionality.

First, the bank will offer different types of loans, with the most common being:

Auto Loans (for vehicles)
Housing Loans
Personal Loans
Loans typically have various fees applied to them, but for simplicity, we’ll assume that a loan has just one fee.

While loans have their own specific characteristics, we will focus only on the aspects they share in common.

These common elements will be stored in the Loan table.

    - LoanOfferId: PRIMARY KEY VARCHAR(64) NOT NULL,
    - Name: VARCHAR(64) NOT NULL,
    - Description: VARCHAR(64) NOT NULL,
    - LoanType: CHAR(2)  NOT NULL, //Auto, Housing, personal
    - MaxEffort: INTEGER NOT NULL, // From 0 to 100
    - Interest: DECIMAL (5,2)
    - IsActive: BOOL NOT NULL,

### Loan

After the client selects a loan offer, a loan entry will be created in the Loan table. The structure for this table will include the following fields:

    - LoanId: PRIMARY KEY VARCHAR(64) NOT NULL,
    - Name: VARCHAR(64) NOT NULL,
    - RelatedAccount: FOREIGN KEY VARCHAR(64) NOT NULL,
    - StartDate: DATE NOT NULL,
    - RelatedOffer: FOREIGN KEY VARCHAR(64) NOT NULL,
    - LoanDuration: INTEGER NOT NULL, // Duration in months
    - ContractedAmount: DECIMAL (20,2) NOT NULL,
    - PaidAmount: DECIMAL (20,2) NOT NULL,

## The controllers

Controllers are classes responsible for handling specific web API requests. In the Data Tier, we will have a controller for each database table, and a controller for authentication:

    - AuthenticationController: For operations related to token generation
    - AccountsController: For operations related to accounts
    - CardsController: For operations related to cards
    - ClientsController: For operations related to clients
    - LoanOffersController: For operations related to loans offers
    - LoansController: For operations related to loans
    - PlasticsController: For operations related to plastics
    - TransactionsController: For operations related to transactions

### AccountsController

    - GetClientAccounts: Retrieves the accounts of a specific client
    - GetAccountById: Retrieves the account of a specified id
    - AddAccount: Creates a new account
    - EditAccount: Edits as existing account
    - DeleteAccount: Deletes an account
