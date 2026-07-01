<script setup>
/**
 * DashboardView.vue
 * Vista principal del panel de control de SafeWatch AI.
 * Incluye tarjetas de métricas, gráfico de actividad por hora,
 * tabla de últimos vehículos detectados conectada al backend.
 */
import { ref, onMounted } from 'vue'
import { API_BASE_URL } from '../config'

// Estado de red para la tabla de vehículos
const loading = ref(true)
const error = ref(null)

// Datos de métricas principales
const metrics = ref([
  {
    id: 'vehicles',
    label: 'Vehículos Detectados Hoy',
    value: '1,248',
    change: '+12.5%',
    trend: 'up',
    color: 'emerald',
    icon: 'car'
  },
  {
    id: 'alerts',
    label: 'Alertas Generadas',
    value: '34',
    change: '+8.2%',
    trend: 'up',
    color: 'amber',
    icon: 'alert'
  },
  {
    id: 'plates',
    label: 'Placas Únicas',
    value: '892',
    change: '+3.1%',
    trend: 'up',
    color: 'blue',
    icon: 'plate'
  },
  {
    id: 'suspicious',
    label: 'Eventos Sospechosos',
    value: '07',
    change: 'Crítico',
    trend: 'critical',
    color: 'red',
    icon: 'warning'
  }
])

// Datos simulados para el gráfico de actividad por hora
const hourlyData = ref([
  { hour: '00', value: 12, label: '12:00 AM' },
  { hour: '01', value: 8, label: '1:00 AM' },
  { hour: '02', value: 5, label: '2:00 AM' },
  { hour: '03', value: 3, label: '3:00 AM' },
  { hour: '04', value: 6, label: '4:00 AM' },
  { hour: '05', value: 15, label: '5:00 AM' },
  { hour: '06', value: 35, label: '6:00 AM' },
  { hour: '07', value: 65, label: '7:00 AM' },
  { hour: '08', value: 85, label: '8:00 AM' },
  { hour: '09', value: 72, label: '9:00 AM' },
  { hour: '10', value: 58, label: '10:00 AM' },
  { hour: '11', value: 62, label: '11:00 AM' },
  { hour: '12', value: 78, label: '12:00 PM' },
  { hour: '13', value: 82, label: '1:00 PM' },
  { hour: '14', value: 70, label: '2:00 PM' },
  { hour: '15', value: 55, label: '3:00 PM' },
  { hour: '16', value: 68, label: '4:00 PM' },
  { hour: '17', value: 90, label: '5:00 PM' },
  { hour: '18', value: 95, label: '6:00 PM' },
  { hour: '19', value: 75, label: '7:00 PM' },
  { hour: '20', value: 50, label: '8:00 PM' },
  { hour: '21', value: 38, label: '9:00 PM' },
  { hour: '22', value: 25, label: '10:00 PM' },
  { hour: '23', value: 18, label: '11:00 PM' }
])

const maxHourlyValue = Math.max(...hourlyData.value.map(d => d.value))

// Datos de los últimos vehículos detectados (Fallback)
const recentVehicles = ref([
  { time: '18:42:15', plate: 'ABC-1234', zone: 'Entrada Principal', status: 'AUTORIZADO' },
  { time: '18:38:07', plate: 'XYZ-9876', zone: 'Zona Industrial C', status: 'SOSPECHOSO' },
  { time: '18:35:22', plate: 'DEF-5678', zone: 'Estacionamiento B', status: 'AUTORIZADO' },
  { time: '18:31:49', plate: 'GHI-3456', zone: 'Perímetro Este', status: 'AUTORIZADO' },
  { time: '18:28:11', plate: 'JKL-7890', zone: 'Entrada Norte', status: 'SOSPECHOSO' },
  { time: '18:24:55', plate: 'MNO-2468', zone: 'Zona de Carga', status: 'AUTORIZADO' }
])

async function fetchRecentVehicles() {
  try {
    loading.value = true
    error.value = null
    const response = await fetch(API_BASE_URL)
    if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`)
    const data = await response.json()
    // Tomar los primeros 6 registros y mapearlos al formato esperado por DashboardView
    recentVehicles.value = data.slice(0, 6).map(e => {
      const dateObj = new Date(e.fechaHoraDeteccion)
      return {
        time: dateObj.toTimeString().split(' ')[0], // Extraer HH:MM:SS
        plate: e.placa,
        zone: e.zonaMonitoreada,
        status: e.estado
      }
    })
  } catch (err) {
    console.error('Error fetching recent vehicles:', err)
    error.value = 'No se pudo conectar con el servidor backend.'
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  fetchRecentVehicles()
})

// Datos de alertas recientes
const recentAlerts = ref([
  {
    id: 1,
    type: 'critical',
    title: 'Placa en Lista Negra',
    description: 'Vehículo XYZ-9876 detectado en Zona Industrial C',
    time: 'Hace 4 min',
    icon: 'shield'
  },
  {
    id: 2,
    type: 'warning',
    title: 'Permanencia Prolongada',
    description: 'Vehículo JKL-7890 lleva 3h en Entrada Norte',
    time: 'Hace 14 min',
    icon: 'clock'
  },
  {
    id: 3,
    type: 'warning',
    title: 'Movimiento Errático',
    description: 'Vehículo RST-1357 patrón irregular detectado',
    time: 'Hace 28 min',
    icon: 'route'
  },
  {
    id: 4,
    type: 'info',
    title: 'Zona Restringida',
    description: 'Acceso no autorizado en Perímetro Sur',
    time: 'Hace 45 min',
    icon: 'zone'
  }
])
</script>

<template>
  <div class="space-y-6">
    <!-- Header -->
    <div class="flex items-center justify-between">
      <div>
        <h1 class="text-2xl font-bold text-slate-100">Panel de Control</h1>
        <p class="text-sm text-slate-400 mt-1">Monitoreo en tiempo real del sistema SafeWatch AI</p>
      </div>
      <div class="flex items-center gap-3">
        <div class="flex items-center gap-2 px-3 py-1.5 bg-emerald-500/10 border border-emerald-500/20 rounded-full">
          <div class="w-2 h-2 rounded-full bg-emerald-400 animate-pulse"></div>
          <span class="text-xs font-medium text-emerald-400">EN VIVO</span>
        </div>
        <span class="text-xs text-slate-500">Última actualización: 18:42:15</span>
      </div>
    </div>

    <!-- Tarjetas de Métricas -->
    <div class="grid grid-cols-1 sm:grid-cols-2 xl:grid-cols-4 gap-4">
      <div
        v-for="metric in metrics"
        :key="metric.id"
        class="relative overflow-hidden bg-slate-800/50 border border-slate-700/50 rounded-xl p-5 backdrop-blur-sm transition-all duration-300 hover:border-slate-600/50 hover:bg-slate-800/70 group"
      >
        <!-- Glow sutil de fondo -->
        <div
          :class="[
            'absolute -top-12 -right-12 w-32 h-32 rounded-full opacity-0 group-hover:opacity-100 transition-opacity duration-500 blur-3xl',
            metric.color === 'emerald' ? 'bg-emerald-500/10' : '',
            metric.color === 'amber' ? 'bg-amber-500/10' : '',
            metric.color === 'blue' ? 'bg-blue-500/10' : '',
            metric.color === 'red' ? 'bg-red-500/10' : ''
          ]"
        ></div>

        <div class="relative flex items-start justify-between">
          <div>
            <p class="text-xs font-medium text-slate-400 uppercase tracking-wider">{{ metric.label }}</p>
            <p class="mt-2 text-3xl font-bold text-slate-100">{{ metric.value }}</p>
          </div>
          <!-- Icono -->
          <div
            :class="[
              'w-10 h-10 rounded-lg flex items-center justify-center',
              metric.color === 'emerald' ? 'bg-emerald-500/10' : '',
              metric.color === 'amber' ? 'bg-amber-500/10' : '',
              metric.color === 'blue' ? 'bg-blue-500/10' : '',
              metric.color === 'red' ? 'bg-red-500/10' : ''
            ]"
          >
            <!-- Car -->
            <svg v-if="metric.icon === 'car'" :class="['w-5 h-5', metric.color === 'emerald' ? 'text-emerald-400' : '']" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
              <path stroke-linecap="round" stroke-linejoin="round" d="M8.25 18.75a1.5 1.5 0 0 1-3 0m3 0a1.5 1.5 0 0 0-3 0m3 0h6m-9 0H3.375a1.125 1.125 0 0 1-1.125-1.125V14.25m17.25 4.5a1.5 1.5 0 0 1-3 0m3 0a1.5 1.5 0 0 0-3 0m3 0H6.375m11.25-4.5A1.125 1.125 0 0 0 16.5 7.875L14.625 3.75h-5.25L7.5 7.875A1.125 1.125 0 0 0 6.375 9v5.25m11.25-4.5V9M3.375 14.25h17.25" />
            </svg>
            <!-- Alert -->
            <svg v-if="metric.icon === 'alert'" :class="['w-5 h-5', metric.color === 'amber' ? 'text-amber-400' : '']" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
              <path stroke-linecap="round" stroke-linejoin="round" d="M14.857 17.082a23.848 23.848 0 0 0 5.454-1.31A8.967 8.967 0 0 1 18 9.75V9A6 6 0 0 0 6 9v.75a8.967 8.967 0 0 1-2.312 6.022c1.733.64 3.56 1.085 5.455 1.31m5.714 0a24.255 24.255 0 0 1-5.714 0m5.714 0a3 3 0 1 1-5.714 0" />
            </svg>
            <!-- Plate -->
            <svg v-if="metric.icon === 'plate'" :class="['w-5 h-5', metric.color === 'blue' ? 'text-blue-400' : '']" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
              <path stroke-linecap="round" stroke-linejoin="round" d="M15 9h3.75M15 12h3.75M15 15h3.75M4.5 19.5h15a2.25 2.25 0 0 0 2.25-2.25V6.75A2.25 2.25 0 0 0 19.5 4.5h-15a2.25 2.25 0 0 0-2.25 2.25v10.5A2.25 2.25 0 0 0 4.5 19.5Zm6-10.125a1.875 1.875 0 1 1-3.75 0 1.875 1.875 0 0 1 3.75 0Zm1.294 6.336a6.721 6.721 0 0 1-3.17.789 6.721 6.721 0 0 1-3.168-.789 3.376 3.376 0 0 1 6.338 0Z" />
            </svg>
            <!-- Warning -->
            <svg v-if="metric.icon === 'warning'" :class="['w-5 h-5', metric.color === 'red' ? 'text-red-400' : '']" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
              <path stroke-linecap="round" stroke-linejoin="round" d="M12 9v3.75m-9.303 3.376c-.866 1.5.217 3.374 1.948 3.374h14.71c1.73 0 2.813-1.874 1.948-3.374L13.949 3.378c-.866-1.5-3.032-1.5-3.898 0L2.697 16.126ZM12 15.75h.007v.008H12v-.008Z" />
            </svg>
          </div>
        </div>

        <!-- Indicador de cambio -->
        <div class="mt-3 flex items-center gap-1.5">
          <span
            v-if="metric.trend === 'critical'"
            class="inline-flex items-center gap-1 px-2 py-0.5 rounded-md text-[10px] font-bold uppercase tracking-wider bg-red-950 text-red-400 border border-red-500/20"
          >
            <svg class="w-3 h-3" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2">
              <path stroke-linecap="round" stroke-linejoin="round" d="M12 9v3.75m-9.303 3.376c-.866 1.5.217 3.374 1.948 3.374h14.71c1.73 0 2.813-1.874 1.948-3.374L13.949 3.378c-.866-1.5-3.032-1.5-3.898 0L2.697 16.126ZM12 15.75h.007v.008H12v-.008Z" />
            </svg>
            {{ metric.change }}
          </span>
          <span
            v-else
            class="inline-flex items-center gap-1 text-xs text-emerald-400"
          >
            <svg class="w-3 h-3" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2">
              <path stroke-linecap="round" stroke-linejoin="round" d="M2.25 18 9 11.25l4.306 4.306a11.95 11.95 0 0 1 5.814-5.518l2.74-1.22m0 0-5.94-2.281m5.94 2.28-2.28 5.941" />
            </svg>
            {{ metric.change }}
          </span>
          <span class="text-[10px] text-slate-500">vs. ayer</span>
        </div>
      </div>
    </div>

    <!-- Contenido Principal: Gráfico + Alertas -->
    <div class="grid grid-cols-1 xl:grid-cols-3 gap-6">
      <!-- Gráfico de Actividad Vehicular + Tabla (2/3 del ancho) -->
      <div class="xl:col-span-2 space-y-6">
        <!-- Gráfico de Barras: Actividad por Hora -->
        <div class="bg-slate-800/50 border border-slate-700/50 rounded-xl p-6 backdrop-blur-sm">
          <div class="flex items-center justify-between mb-6">
            <div>
              <h2 class="text-lg font-semibold text-slate-100">Actividad de Vehículos por Hora</h2>
              <p class="text-xs text-slate-400 mt-1">Distribución de detecciones en las últimas 24 horas</p>
            </div>
            <div class="flex items-center gap-4 text-xs">
              <div class="flex items-center gap-1.5">
                <div class="w-2.5 h-2.5 rounded-sm bg-emerald-500"></div>
                <span class="text-slate-400">Normal</span>
              </div>
              <div class="flex items-center gap-1.5">
                <div class="w-2.5 h-2.5 rounded-sm bg-amber-500"></div>
                <span class="text-slate-400">Moderado</span>
              </div>
              <div class="flex items-center gap-1.5">
                <div class="w-2.5 h-2.5 rounded-sm bg-red-500"></div>
                <span class="text-slate-400">Alto</span>
              </div>
            </div>
          </div>

          <!-- Barras del gráfico -->
          <div class="flex items-end gap-1 h-48">
            <div
              v-for="data in hourlyData"
              :key="data.hour"
              class="flex-1 flex flex-col items-center gap-1 group/bar"
            >
              <!-- Tooltip -->
              <div class="opacity-0 group-hover/bar:opacity-100 transition-opacity text-[10px] text-slate-300 bg-slate-900 px-1.5 py-0.5 rounded border border-slate-700 whitespace-nowrap pointer-events-none">
                {{ data.value }}
              </div>
              <!-- Barra -->
              <div
                class="w-full rounded-t transition-all duration-300 group-hover/bar:opacity-80 cursor-pointer"
                :class="[
                  data.value >= 80 ? 'bg-gradient-to-t from-red-600 to-red-400' :
                  data.value >= 50 ? 'bg-gradient-to-t from-amber-600 to-amber-400' :
                  'bg-gradient-to-t from-emerald-600 to-emerald-400'
                ]"
                :style="{ height: (data.value / maxHourlyValue * 100) + '%', minHeight: '4px' }"
              ></div>
              <!-- Label de hora -->
              <span class="text-[9px] text-slate-500 mt-0.5">{{ data.hour }}</span>
            </div>
          </div>
        </div>

        <!-- Tabla de Últimos Vehículos Detectados -->
        <div class="bg-slate-800/50 border border-slate-700/50 rounded-xl backdrop-blur-sm overflow-hidden relative">
          <!-- Loading overlay -->
          <div v-if="loading" class="absolute inset-0 bg-slate-900/60 backdrop-blur-sm flex flex-col items-center justify-center z-10">
            <div class="w-8 h-8 border-4 border-emerald-500 border-t-transparent rounded-full animate-spin mb-2"></div>
            <span class="text-xs text-emerald-400 font-medium tracking-wide">ACTUALIZANDO DATOS...</span>
          </div>

          <div class="px-6 py-4 border-b border-slate-700/50 flex justify-between items-center">
            <div>
              <h2 class="text-lg font-semibold text-slate-100">Últimos Vehículos Detectados</h2>
              <p class="text-xs text-slate-400 mt-1">Registro en tiempo real de detecciones del sistema</p>
            </div>
            <div v-if="error" class="flex items-center text-amber-400 text-xs">
              <svg class="w-4 h-4 mr-1.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z"/>
              </svg>
              <span>{{ error }}</span>
            </div>
          </div>
          <div class="overflow-x-auto">
            <table class="w-full">
              <thead>
                <tr class="border-b border-slate-700/50">
                  <th class="px-6 py-3 text-left text-[10px] font-semibold text-slate-400 uppercase tracking-wider">Hora</th>
                  <th class="px-6 py-3 text-left text-[10px] font-semibold text-slate-400 uppercase tracking-wider">Placa</th>
                  <th class="px-6 py-3 text-left text-[10px] font-semibold text-slate-400 uppercase tracking-wider">Zona</th>
                  <th class="px-6 py-3 text-left text-[10px] font-semibold text-slate-400 uppercase tracking-wider">Estado</th>
                </tr>
              </thead>
              <tbody class="divide-y divide-slate-700/30">
                <tr
                  v-for="(vehicle, idx) in recentVehicles"
                  :key="idx"
                  class="hover:bg-slate-700/20 transition-colors duration-150"
                >
                  <td class="px-6 py-3">
                    <span class="text-sm font-mono text-slate-300">{{ vehicle.time }}</span>
                  </td>
                  <td class="px-6 py-3">
                    <span class="inline-flex items-center px-2.5 py-1 rounded-md bg-slate-700/50 text-sm font-mono font-semibold text-slate-200 border border-slate-600/30">
                      {{ vehicle.plate }}
                    </span>
                  </td>
                  <td class="px-6 py-3">
                    <span class="text-sm text-slate-300">{{ vehicle.zone }}</span>
                  </td>
                  <td class="px-6 py-3">
                    <span
                      :class="[
                        'inline-flex items-center gap-1.5 px-2.5 py-1 rounded-full text-[11px] font-bold uppercase tracking-wider',
                        vehicle.status === 'AUTORIZADO'
                          ? 'bg-emerald-950 text-emerald-400 border border-emerald-500/20'
                          : vehicle.status === 'SOSPECHOSO' 
                            ? 'bg-red-950 text-red-400 border border-red-500/20'
                            : 'bg-amber-950 text-amber-400 border border-amber-500/20'
                      ]"
                    >
                      <span
                        :class="[
                          'w-1.5 h-1.5 rounded-full',
                          vehicle.status === 'AUTORIZADO' ? 'bg-emerald-400' : 
                          vehicle.status === 'SOSPECHOSO' ? 'bg-red-400' : 'bg-amber-400'
                        ]"
                      ></span>
                      {{ vehicle.status }}
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>

      <!-- Panel de Alertas Recientes (1/3 del ancho) -->
      <div class="xl:col-span-1">
        <div class="bg-slate-800/50 border border-slate-700/50 rounded-xl backdrop-blur-sm overflow-hidden h-full">
          <div class="px-6 py-4 border-b border-slate-700/50 flex items-center justify-between">
            <div>
              <h2 class="text-lg font-semibold text-slate-100">Alertas Recientes</h2>
              <p class="text-xs text-slate-400 mt-1">Notificaciones del sistema</p>
            </div>
            <span class="inline-flex items-center justify-center w-6 h-6 rounded-full bg-red-500/20 text-[10px] font-bold text-red-400">
              {{ recentAlerts.length }}
            </span>
          </div>

          <div class="p-4 space-y-3">
            <div
              v-for="alert in recentAlerts"
              :key="alert.id"
              :class="[
                'p-4 rounded-lg border transition-all duration-200 hover:scale-[1.01] cursor-pointer',
                alert.type === 'critical'
                  ? 'bg-red-950/50 border-red-500/20 hover:border-red-500/40'
                  : alert.type === 'warning'
                    ? 'bg-amber-950/30 border-amber-500/15 hover:border-amber-500/30'
                    : 'bg-slate-700/30 border-slate-600/20 hover:border-slate-500/30'
              ]"
            >
              <div class="flex items-start gap-3">
                <!-- Icono de alerta -->
                <div
                  :class="[
                    'w-8 h-8 rounded-lg flex items-center justify-center flex-shrink-0 mt-0.5',
                    alert.type === 'critical' ? 'bg-red-500/20' :
                    alert.type === 'warning' ? 'bg-amber-500/20' : 'bg-slate-600/30'
                  ]"
                >
                  <!-- Shield -->
                  <svg v-if="alert.icon === 'shield'" class="w-4 h-4 text-red-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M12 9v3.75m0-10.036A11.959 11.959 0 0 1 3.598 6 11.99 11.99 0 0 0 3 9.75c0 5.592 3.824 10.29 9 11.623 5.176-1.332 9-6.03 9-11.622 0-1.31-.21-2.571-.598-3.751h-.152c-3.196 0-6.1-1.248-8.25-3.285Z" />
                  </svg>
                  <!-- Clock -->
                  <svg v-if="alert.icon === 'clock'" class="w-4 h-4 text-amber-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M12 6v6h4.5m4.5 0a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
                  </svg>
                  <!-- Route -->
                  <svg v-if="alert.icon === 'route'" class="w-4 h-4 text-amber-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M9 6.75V15m0 0-3-3m3 3 3-3m6-7.5V15m0 0-3-3m3 3 3-3" />
                  </svg>
                  <!-- Zone -->
                  <svg v-if="alert.icon === 'zone'" class="w-4 h-4 text-slate-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M15 10.5a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
                    <path stroke-linecap="round" stroke-linejoin="round" d="M19.5 10.5c0 7.142-7.5 11.25-7.5 11.25S4.5 17.642 4.5 10.5a7.5 7.5 0 1 1 15 0Z" />
                  </svg>
                </div>

                <div class="flex-1 min-w-0">
                  <h3
                    :class="[
                      'text-sm font-semibold',
                      alert.type === 'critical' ? 'text-red-400' :
                      alert.type === 'warning' ? 'text-amber-400' : 'text-slate-300'
                    ]"
                  >
                    {{ alert.title }}
                  </h3>
                  <p class="text-xs text-slate-400 mt-1 leading-relaxed">{{ alert.description }}</p>
                  <span class="text-[10px] text-slate-500 mt-2 inline-block">{{ alert.time }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
