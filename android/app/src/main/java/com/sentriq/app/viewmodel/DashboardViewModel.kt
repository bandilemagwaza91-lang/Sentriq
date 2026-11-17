package com.sentriq.app.viewmodel


import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sentriq.app.data.remote.AlertResponse
import com.sentriq.app.data.remote.TransactionResponse
import com.sentriq.app.data.repository.SentriqRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

data class DashboardUiState(
    val transactions: List<TransactionResponse> = emptyList(),
    val alerts: List<AlertResponse> = emptyList(),
    val isLoading: Boolean = false,
    val errorMessage: String? = null
)

class DashboardViewModel(
    private val repository: SentriqRepository = SentriqRepository()
) : ViewModel() {

    private val _uiState = MutableStateFlow(DashboardUiState())
    val uiState: StateFlow<DashboardUiState> = _uiState.asStateFlow()

    init {
        refresh()
    }

    fun refresh() {
        viewModelScope.launch {
            _uiState.update { it.copy(isLoading = true, errorMessage = null) }

            val transactionsResult = repository.getTransactions()
            val alertsResult = repository.getAlerts()

            _uiState.update {
                it.copy(
                    transactions = transactionsResult.getOrDefault(emptyList()),
                    alerts = alertsResult.getOrDefault(emptyList()),
                    isLoading = false,
                    errorMessage = if (transactionsResult.isFailure || alertsResult.isFailure) {
                        "Could not reach the server"
                    } else {
                        null
                    }
                )
            }
        }
    }
}