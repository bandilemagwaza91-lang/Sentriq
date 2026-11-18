package com.sentriq.app.ui.screens



import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.Button
import androidx.compose.material3.Card
import androidx.compose.material3.LinearProgressIndicator
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import androidx.lifecycle.viewmodel.compose.viewModel
import com.sentriq.app.data.remote.AlertResponse
import com.sentriq.app.ui.theme.SentriqTheme
import com.sentriq.app.viewmodel.DashboardUiState
import com.sentriq.app.viewmodel.DashboardViewModel

@Composable
fun DashboardScreen(
    modifier: Modifier = Modifier,
    viewModel: DashboardViewModel = viewModel()
) {
    val state by viewModel.uiState.collectAsStateWithLifecycle()
    DashboardContent(state = state, onRefresh = viewModel::refresh, modifier = modifier)
}

@Composable
fun DashboardContent(
    state: DashboardUiState,
    onRefresh: () -> Unit,
    modifier: Modifier = Modifier
) {
    Column(modifier = modifier.fillMaxSize().padding(16.dp)) {
        Row(
            modifier = Modifier.fillMaxWidth(),
            horizontalArrangement = Arrangement.SpaceBetween
        ) {
            Text("Sentriq Alerts", style = MaterialTheme.typography.headlineSmall)
            Button(onClick = onRefresh) { Text("Refresh") }
        }

        if (state.isLoading) {
            LinearProgressIndicator(modifier = Modifier.fillMaxWidth())
        }

        state.errorMessage?.let {
            Text(it, color = MaterialTheme.colorScheme.error)
        }

        LazyColumn {
            items(state.alerts) { alert ->
                Card(modifier = Modifier.fillMaxWidth().padding(vertical = 4.dp)) {
                    Column(modifier = Modifier.padding(12.dp)) {
                        Text(alert.severity, style = MaterialTheme.typography.labelLarge)
                        Text(alert.reason)
                    }
                }
            }
        }
    }
}

@Preview(showBackground = true)
@Composable
fun DashboardPreview() {
    SentriqTheme {
        DashboardContent(
            state = DashboardUiState(
                alerts = listOf(
                    AlertResponse(1, 2, "Large amount: 15,000.00 is over the 10,000.00 limit", "High", "2026-11-18T10:00:00Z"),
                    AlertResponse(2, 3, "Location change: Durban to Johannesburg within 10 minutes", "Medium", "2026-11-18T10:02:00Z")
                )
            ),
            onRefresh = {}
        )
    }
}