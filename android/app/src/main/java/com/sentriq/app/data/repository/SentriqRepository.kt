
package com.sentriq.app.data.repository

import com.sentriq.app.data.remote.AlertResponse
import com.sentriq.app.data.remote.CreateTransactionRequest
import com.sentriq.app.data.remote.RetrofitClient
import com.sentriq.app.data.remote.SentriqApi
import com.sentriq.app.data.remote.TransactionResponse

class SentriqRepository(
    private val api: SentriqApi = RetrofitClient.api
) {

    suspend fun getTransactions(): Result<List<TransactionResponse>> =
        runCatching { api.getTransactions() }

    suspend fun getAlerts(): Result<List<AlertResponse>> =
        runCatching { api.getAlerts() }

    suspend fun createTransaction(
        request: CreateTransactionRequest
    ): Result<TransactionResponse> =
        runCatching { api.createTransaction(request) }
}
