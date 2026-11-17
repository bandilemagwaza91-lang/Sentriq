package com.sentriq.app.data.remote

import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST

interface SentriqApi {

    @POST("api/transactions")
    suspend fun createTransaction(
        @Body request: CreateTransactionRequest
    ): TransactionResponse

    @GET("api/transactions")
    suspend fun getTransactions(): List<TransactionResponse>

    @GET("api/alerts")
    suspend fun getAlerts(): List<AlertResponse>
}
