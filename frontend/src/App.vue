<script setup>
    /**
     * App.vue
     * Componente raíz de SafeWatch AI.
     * Integra la barra lateral con las vistas del sistema.
     */

    import { ref } from 'vue'

    import Sidebar from './components/Sidebar.vue'
    import DashboardView from './views/DashboardView.vue'
    import EventsView from './views/EventsView.vue'
    import AlertsView from './views/AlertsView.vue'
    import VideoAnalysisView from './views/VideoAnalysisView.vue'

    // Estado reactivo para la navegación
    const currentView = ref('dashboard')

    function handleNavigate(viewId) {
        currentView.value = viewId
    }
</script>

<template>
    <div class="flex min-h-screen bg-slate-900 text-slate-100">

        <!-- Sidebar -->
        <Sidebar :currentView="currentView"
                 @navigate="handleNavigate" />

        <!-- Contenido principal -->
        <main class="flex-1 overflow-y-auto">
            <div class="p-6 lg:p-8 max-w-[1600px] mx-auto">

                <!-- Dashboard -->
                <DashboardView v-if="currentView === 'dashboard'" />

                <!-- Eventos -->
                <EventsView v-else-if="currentView === 'events'" />

                <!-- Alertas -->
                <AlertsView v-else-if="currentView === 'alerts'" />

                <!-- Análisis de Video -->
                <VideoAnalysisView v-else-if="currentView === 'settings'" />

            </div>
        </main>

    </div>
</template>