package com.sentriq.app.data.remote

data class CreateTransactionRequest(
    val accountId: String,
    val amount: Double,
    val merchant: String,
    val location: String
)

data class TransactionResponse(
    val id: Int,
    val accountId: String,
    val amount: Double,
    val merchant: String,
    val location: String,
    val timestamp: String,
    val isFlagged: Boolean
)

data class AlertResponse(
    val id: Int,
    val transactionId: Int,
    val reason: String,
    val severity: String,
    val createdAt: String
)