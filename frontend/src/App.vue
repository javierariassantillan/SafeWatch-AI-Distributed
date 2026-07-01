<script setup>
/**
 * App.vue
 * Componente raíz de SafeWatch AI.
 * Integra la barra lateral (Sidebar) con las vistas del sistema,
 * manejando la navegación mediante estado reactivo.
 */
import { ref } from 'vue'
import Sidebar from './components/Sidebar.vue'
import DashboardView from './views/DashboardView.vue'
import EventsView from './views/EventsView.vue'
import AlertsView from './views/AlertsView.vue'

// Estado reactivo para la navegación
const currentView = ref('dashboard')

function handleNavigate(viewId) {
  currentView.value = viewId
}
</script>

<template>
  <div class="flex min-h-screen bg-slate-900 text-slate-100">
    <!-- Sidebar fijo a la izquierda -->
    <Sidebar :currentView="currentView" @navigate="handleNavigate" />

    <!-- Contenido principal -->
    <main class="flex-1 overflow-y-auto">
      <div class="p-6 lg:p-8 max-w-[1600px] mx-auto">
        <!-- Renderizado dinámico según la vista seleccionada -->
        <DashboardView v-if="currentView === 'dashboard'" />
        <EventsView v-else-if="currentView === 'events'" />
        <AlertsView v-else-if="currentView === 'alerts'" />

        <!-- Vista de Configuración (placeholder) -->
        <div v-else-if="currentView === 'settings'" class="flex items-center justify-center min-h-[60vh]">
          <div class="text-center">
            <div class="w-16 h-16 rounded-2xl bg-slate-800/50 border border-slate-700/50 flex items-center justify-center mx-auto mb-4">
              <svg class="w-8 h-8 text-slate-500" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                <path stroke-linecap="round" stroke-linejoin="round"
                  d="M9.594 3.94c.09-.542.56-.94 1.11-.94h2.593c.55 0 1.02.398 1.11.94l.213 1.281c.063.374.313.686.645.87.074.04.147.083.22.127.325.196.72.257 1.075.124l1.217-.456a1.125 1.125 0 0 1 1.37.49l1.296 2.247a1.125 1.125 0 0 1-.26 1.431l-1.003.827c-.293.241-.438.613-.43.992a7.723 7.723 0 0 1 0 .255c-.008.378.137.75.43.991l1.004.827c.424.35.534.955.26 1.43l-1.298 2.247a1.125 1.125 0 0 1-1.369.491l-1.217-.456c-.355-.133-.75-.072-1.076.124a6.47 6.47 0 0 1-.22.128c-.331.183-.581.495-.644.869l-.213 1.281c-.09.543-.56.94-1.11.94h-2.594c-.55 0-1.019-.398-1.11-.94l-.213-1.281c-.062-.374-.312-.686-.644-.87a6.52 6.52 0 0 1-.22-.127c-.325-.196-.72-.257-1.076-.124l-1.217.456a1.125 1.125 0 0 1-1.369-.49l-1.297-2.247a1.125 1.125 0 0 1 .26-1.431l1.004-.827c.292-.24.437-.613.43-.991a6.932 6.932 0 0 1 0-.255c.007-.38-.138-.751-.43-.992l-1.004-.827a1.125 1.125 0 0 1-.26-1.43l1.297-2.247a1.125 1.125 0 0 1 1.37-.491l1.216.456c.356.133.751.072 1.076-.124.072-.044.146-.086.22-.128.332-.183.582-.495.644-.869l.214-1.28Z" />
                <path stroke-linecap="round" stroke-linejoin="round" d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
              </svg>
            </div>
            <h2 class="text-xl font-semibold text-slate-300 mb-2">Configuración</h2>
            <p class="text-sm text-slate-500">Módulo en desarrollo — Próximamente disponible</p>
          </div>
        </div>
      </div>
    </main>
  </div>
</template>
