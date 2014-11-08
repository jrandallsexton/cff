cff
===

Cash Flow Forecasting

WORK IN PROGRESS.  NOT FOR PUBLIC CONSUMPTION (yet).

After purchasing a major COTS application (sounds like Rick Cooks), I was sorely disappointed to learn that the cash flow modeling had been removed.  This was a major need of mine for my own personal finances; this projet attempts to scratch that itch.

The initial thoughts in terms of stages of development will/should/might look a little something like:
1.  Get the modeler to perform basic/crude forecasting (simple credit/debit processing)
2.  Expand the modeler to be able to handle installment loans (i.e. fixed payment/duration/interest rate)
3.  Expand the modeler to be able to handle revolving accounts (i.e. variable interest rate | calculated balances | calculated payoff dates)
4.  Introduce a data repository so that you can persist different cash flow models (needed for future feature of model output comparisons)
5.  Introduce expenditure analysis thereby allowing the modeler to indicate that despite your stated expenditures, based on what it sees from historical data, your projected CFF is X instead of the straight-forward Y
6.  Investigate different mathematical formulas for calculating cash flows; implement them
