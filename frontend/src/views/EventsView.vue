<script setup>
/**
 * EventsView.vue
 * Pantalla de eventos detectados — Registro completo de vehículos identificados.
 * Incluye filtros de búsqueda simulados y tabla detallada conectada al backend.
 */
import { ref, computed, onMounted } from 'vue'
import { API_BASE_URL } from '../config'

// Estado de la red
const loading = ref(true)
const error = ref(null)

// Estado de los filtros
const searchPlate = ref('')
const selectedZone = ref('')
const selectedStatus = ref('')

// Opciones de zonas
const zones = [
  'Sector Industrial C',
  'Área de Carga',
  'Entrada Norte-1',
  'Entrada Sur-2',
  'Entrada Principal',
  'Entrada Norte',
  'Zona Industrial C',
  'Estacionamiento B',
  'Perímetro Este',
  'Perímetro Sur',
  'Zona de Carga',
  'Acceso VIP'
]

// Datos estáticos de eventos (Fallback)
const events = ref([
  {
    id: 1,
    fechaHoraDeteccion: '2025-06-29 18:42:15',
    placa: 'ABC-1234',
    zonaMonitoreada: 'Entrada Principal',
    tipoVehiculo: 'Sedán',
    estado: 'AUTORIZADO',
    indiceRiesgo: 'Bajo'
  },
  {
    id: 2,
    fechaHoraDeteccion: '2025-06-29 18:38:07',
    placa: 'XYZ-9876',
    zonaMonitoreada: 'Zona Industrial C',
    tipoVehiculo: 'Camioneta',
    estado: 'SOSPECHOSO',
    indiceRiesgo: 'Alto'
  },
  {
    id: 3,
    fechaHoraDeteccion: '2025-06-29 18:35:22',
    placa: 'DEF-5678',
    zonaMonitoreada: 'Estacionamiento B',
    tipoVehiculo: 'SUV',
    estado: 'AUTORIZADO',
    indiceRiesgo: 'Bajo'
  },
  {
    id: 4,
    fechaHoraDeteccion: '2025-06-29 18:31:49',
    placa: 'GHI-3456',
    zonaMonitoreada: 'Perímetro Este',
    tipoVehiculo: 'Sedán',
    estado: 'AUTORIZADO',
    indiceRiesgo: 'Bajo'
  },
  {
    id: 5,
    fechaHoraDeteccion: '2025-06-29 18:28:11',
    placa: 'JKL-7890',
    zonaMonitoreada: 'Entrada Norte',
    tipoVehiculo: 'Pickup',
    estado: 'SOSPECHOSO',
    indiceRiesgo: 'Alto'
  },
  {
    id: 6,
    fechaHoraDeteccion: '2025-06-29 18:24:55',
    placa: 'MNO-2468',
    zonaMonitoreada: 'Zona de Carga',
    tipoVehiculo: 'Camión',
    estado: 'AUTORIZADO',
    indiceRiesgo: 'Bajo'
  },
  {
    id: 7,
    fechaHoraDeteccion: '2025-06-29 18:19:33',
    placa: 'PQR-1357',
    zonaMonitoreada: 'Acceso VIP',
    tipoVehiculo: 'Sedán Ejecutivo',
    estado: 'AUTORIZADO',
    indiceRiesgo: 'Bajo'
  },
  {
    id: 8,
    fechaHoraDeteccion: '2025-06-29 18:14:08',
    placa: 'STU-8642',
    zonaMonitoreada: 'Perímetro Sur',
    tipoVehiculo: 'Van',
    estado: 'SOSPECHOSO',
    indiceRiesgo: 'Medio'
  }
])

async function fetchVehicleEvents() {
  try {
    loading.value = true
    error.value = null
    const response = await fetch(API_BASE_URL)
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`)
    }
    const data = await response.json()
    // Mapeo de datos del backend
    events.value = data.map(e => {
      const dateObj = new Date(e.fechaHoraDeteccion)
      return {
        ...e,
        // Formateo para la vista
        fechaHoraDeteccion: dateObj.toISOString().replace('T', ' ').substring(0, 19)
      }
    })
  } catch (err) {
    console.error('Error fetching vehicle events:', err)
    error.value = 'No se pudo conectar con el servidor backend. Mostrando datos de simulación.'
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  fetchVehicleEvents()
})

// Filtrado reactivo
const filteredEvents = computed(() => {
  return events.value.filter(event => {
    const matchPlate = !searchPlate.value ||
      event.placa.toLowerCase().includes(searchPlate.value.toLowerCase())
    const matchZone = !selectedZone.value || event.zonaMonitoreada === selectedZone.value
    const matchStatus = !selectedStatus.value || event.estado === selectedStatus.value
    return matchPlate && matchZone && matchStatus
  })
})

// Contadores
const totalEvents = computed(() => filteredEvents.value.length)
const authorizedCount = computed(() => filteredEvents.value.filter(e => e.estado === 'AUTORIZADO').length)
const suspiciousCount = computed(() => filteredEvents.value.filter(e => e.estado === 'SOSPECHOSO').length)

function clearFilters() {
  searchPlate.value = ''
  selectedZone.value = ''
  selectedStatus.value = ''
}
</script>

<template>
  <div class="space-y-6">
    <!-- Header -->
    <div class="flex items-center justify-between flex-wrap gap-4">
      <div>
        <h1 class="text-2xl font-bold text-slate-100">Eventos Detectados</h1>
        <p class="text-sm text-slate-400 mt-1">Registro completo de vehículos identificados por el sistema</p>
      </div>
      <div class="flex items-center gap-3">
        <!-- Contadores de resumen -->
        <div class="flex items-center gap-2 px-3 py-1.5 bg-slate-800/60 border border-slate-700/50 rounded-lg">
          <span class="text-xs text-slate-400">Total:</span>
          <span class="text-sm font-bold text-slate-200">{{ totalEvents }}</span>
        </div>
        <div class="flex items-center gap-2 px-3 py-1.5 bg-emerald-950/50 border border-emerald-500/20 rounded-lg">
          <span class="w-1.5 h-1.5 rounded-full bg-emerald-400"></span>
          <span class="text-xs text-emerald-400 font-medium">{{ authorizedCount }}</span>
        </div>
        <div class="flex items-center gap-2 px-3 py-1.5 bg-red-950/50 border border-red-500/20 rounded-lg">
          <span class="w-1.5 h-1.5 rounded-full bg-red-400"></span>
          <span class="text-xs text-red-400 font-medium">{{ suspiciousCount }}</span>
        </div>
      </div>
    </div>

    <!-- Mensajes de estado de red -->
    <div v-if="loading" class="flex items-center justify-center p-4 bg-slate-800/50 rounded-xl border border-emerald-500/30">
      <div class="w-5 h-5 border-2 border-emerald-400 border-t-transparent rounded-full animate-spin mr-3"></div>
      <span class="text-emerald-400 font-medium text-sm">Cargando eventos en tiempo real...</span>
    </div>
    
    <div v-if="error" class="flex items-center p-4 bg-amber-950/30 rounded-xl border border-amber-500/30">
      <svg class="w-5 h-5 text-amber-400 mr-3" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
        <path stroke-linecap="round" stroke-linejoin="round" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
      </svg>
      <span class="text-amber-400 font-medium text-sm">{{ error }}</span>
    </div>

    <!-- Filtros de Búsqueda -->
    <div class="bg-slate-800/50 border border-slate-700/50 rounded-xl p-5 backdrop-blur-sm">
      <div class="flex items-center gap-2 mb-4">
        <svg class="w-4 h-4 text-slate-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
          <path stroke-linecap="round" stroke-linejoin="round" d="M12 3c2.755 0 5.455.232 8.083.678.533.09.917.556.917 1.096v1.044a2.25 2.25 0 0 1-.659 1.591l-5.432 5.432a2.25 2.25 0 0 0-.659 1.591v2.927a2.25 2.25 0 0 1-1.244 2.013L9.75 21v-6.568a2.25 2.25 0 0 0-.659-1.591L3.659 7.409A2.25 2.25 0 0 1 3 5.818V4.774c0-.54.384-1.006.917-1.096A48.32 48.32 0 0 1 12 3Z" />
        </svg>
        <h3 class="text-sm font-semibold text-slate-300">Filtros de Búsqueda</h3>
      </div>
      <div class="grid grid-cols-1 sm:grid-cols-3 gap-4">
        <!-- Buscar Placa -->
        <div class="relative">
          <label class="block text-[10px] font-semibold text-slate-400 uppercase tracking-wider mb-1.5">Buscar Placa</label>
          <div class="relative">
            <svg class="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-slate-500" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
              <path stroke-linecap="round" stroke-linejoin="round" d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z" />
            </svg>
            <input
              v-model="searchPlate"
              type="text"
              placeholder="Ej. ABC-1234"
              class="w-full pl-10 pr-4 py-2.5 bg-slate-900/70 border border-slate-600/40 rounded-lg text-sm text-slate-200 placeholder-slate-500 focus:outline-none focus:ring-2 focus:ring-emerald-500/30 focus:border-emerald-500/50 transition-all duration-200"
            />
          </div>
        </div>

        <!-- Seleccionar Zona -->
        <div>
          <label class="block text-[10px] font-semibold text-slate-400 uppercase tracking-wider mb-1.5">Seleccionar Zona</label>
          <select
            v-model="selectedZone"
            class="w-full px-4 py-2.5 bg-slate-900/70 border border-slate-600/40 rounded-lg text-sm text-slate-200 focus:outline-none focus:ring-2 focus:ring-emerald-500/30 focus:border-emerald-500/50 transition-all duration-200 appearance-none cursor-pointer"
          >
            <option value="">Todas las zonas</option>
            <option v-for="zone in zones" :key="zone" :value="zone">{{ zone }}</option>
          </select>
        </div>

        <!-- Seleccionar Estado -->
        <div>
          <label class="block text-[10px] font-semibold text-slate-400 uppercase tracking-wider mb-1.5">Seleccionar Estado</label>
          <div class="flex items-center gap-2">
            <select
              v-model="selectedStatus"
              class="flex-1 px-4 py-2.5 bg-slate-900/70 border border-slate-600/40 rounded-lg text-sm text-slate-200 focus:outline-none focus:ring-2 focus:ring-emerald-500/30 focus:border-emerald-500/50 transition-all duration-200 appearance-none cursor-pointer"
            >
              <option value="">Todos</option>
              <option value="AUTORIZADO">Autorizado</option>
              <option value="SOSPECHOSO">Sospechoso</option>
            </select>
            <button
              @click="clearFilters"
              class="px-3 py-2.5 bg-slate-700/50 border border-slate-600/40 rounded-lg text-xs text-slate-400 hover:bg-slate-600/50 hover:text-slate-200 transition-all duration-200"
              title="Limpiar filtros"
            >
              <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                <path stroke-linecap="round" stroke-linejoin="round" d="M6 18 18 6M6 6l12 12" />
              </svg>
            </button>
          </div>
        </div>
      </div>
    </div>

    <!-- Tabla de Eventos -->
    <div class="bg-slate-800/50 border border-slate-700/50 rounded-xl backdrop-blur-sm overflow-hidden">
      <div class="overflow-x-auto">
        <table class="w-full">
          <thead>
            <tr class="border-b border-slate-700/50 bg-slate-800/80">
              <th class="px-6 py-4 text-left text-[10px] font-semibold text-slate-400 uppercase tracking-wider">Fecha / Hora</th>
              <th class="px-6 py-4 text-left text-[10px] font-semibold text-slate-400 uppercase tracking-wider">Placa</th>
              <th class="px-6 py-4 text-left text-[10px] font-semibold text-slate-400 uppercase tracking-wider">Zona</th>
              <th class="px-6 py-4 text-left text-[10px] font-semibold text-slate-400 uppercase tracking-wider">Tipo de Vehículo</th>
              <th class="px-6 py-4 text-left text-[10px] font-semibold text-slate-400 uppercase tracking-wider">Estado</th>
              <th class="px-6 py-4 text-center text-[10px] font-semibold text-slate-400 uppercase tracking-wider">Acción</th>
            </tr>
          </thead>
          <tbody class="divide-y divide-slate-700/30">
            <tr
              v-for="event in filteredEvents"
              :key="event.id"
              class="hover:bg-slate-700/20 transition-colors duration-150 group"
            >
              <!-- Fecha/Hora -->
              <td class="px-6 py-4">
                <div class="flex flex-col">
                  <span class="text-sm text-slate-200">{{ event.fechaHoraDeteccion.split(' ')[0] }}</span>
                  <span class="text-xs text-slate-400 font-mono">{{ event.fechaHoraDeteccion.split(' ')[1] }}</span>
                </div>
              </td>

              <!-- Placa -->
              <td class="px-6 py-4">
                <span class="inline-flex items-center px-3 py-1.5 rounded-md bg-slate-700/60 text-sm font-mono font-bold text-slate-100 border border-slate-600/30 tracking-wider">
                  {{ event.placa }}
                </span>
              </td>

              <!-- Zona -->
              <td class="px-6 py-4">
                <div class="flex items-center gap-2">
                  <svg class="w-4 h-4 text-slate-500 flex-shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M15 10.5a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
                    <path stroke-linecap="round" stroke-linejoin="round" d="M19.5 10.5c0 7.142-7.5 11.25-7.5 11.25S4.5 17.642 4.5 10.5a7.5 7.5 0 1 1 15 0Z" />
                  </svg>
                  <span class="text-sm text-slate-300">{{ event.zonaMonitoreada }}</span>
                </div>
              </td>

              <!-- Tipo de Vehículo -->
              <td class="px-6 py-4">
                <span class="text-sm text-slate-300">{{ event.tipoVehiculo }}</span>
              </td>

              <!-- Estado -->
              <td class="px-6 py-4">
                <span
                  :class="[
                    'inline-flex items-center gap-1.5 px-3 py-1 rounded-full text-[11px] font-bold uppercase tracking-wider',
                    event.estado === 'AUTORIZADO'
                      ? 'bg-emerald-950 text-emerald-400 border border-emerald-500/20'
                      : event.estado === 'SOSPECHOSO' 
                        ? 'bg-red-950 text-red-400 border border-red-500/20'
                        : 'bg-amber-950 text-amber-400 border border-amber-500/20'
                  ]"
                >
                  <span
                    :class="[
                      'w-1.5 h-1.5 rounded-full',
                      event.estado === 'AUTORIZADO' ? 'bg-emerald-400' : 
                      event.estado === 'SOSPECHOSO' ? 'bg-red-400 animate-pulse' : 'bg-amber-400'
                    ]"
                  ></span>
                  {{ event.estado }}
                </span>
              </td>

              <!-- Acción -->
              <td class="px-6 py-4 text-center">
                <div class="flex items-center justify-center gap-2">
                  <button
                    class="p-2 rounded-lg text-slate-400 hover:bg-slate-700/60 hover:text-emerald-400 transition-all duration-200"
                    title="Ver detalles"
                  >
                    <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                      <path stroke-linecap="round" stroke-linejoin="round" d="M2.036 12.322a1.012 1.012 0 0 1 0-.639C3.423 7.51 7.36 4.5 12 4.5c4.638 0 8.573 3.007 9.963 7.178.07.207.07.431 0 .639C20.577 16.49 16.64 19.5 12 19.5c-4.638 0-8.573-3.007-9.963-7.178Z" />
                      <path stroke-linecap="round" stroke-linejoin="round" d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
                    </svg>
                  </button>
                  <button
                    v-if="event.estado === 'SOSPECHOSO'"
                    class="p-2 rounded-lg text-slate-400 hover:bg-red-950/50 hover:text-red-400 transition-all duration-200"
                    title="Investigar"
                  >
                    <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                      <path stroke-linecap="round" stroke-linejoin="round" d="M12 9v3.75m-9.303 3.376c-.866 1.5.217 3.374 1.948 3.374h14.71c1.73 0 2.813-1.874 1.948-3.374L13.949 3.378c-.866-1.5-3.032-1.5-3.898 0L2.697 16.126ZM12 15.75h.007v.008H12v-.008Z" />
                    </svg>
                  </button>
                </div>
              </td>
            </tr>

            <!-- Estado vacío -->
            <tr v-if="filteredEvents.length === 0 && !loading">
              <td colspan="6" class="px-6 py-12 text-center">
                <div class="flex flex-col items-center gap-3">
                  <svg class="w-12 h-12 text-slate-600" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1">
                    <path stroke-linecap="round" stroke-linejoin="round" d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z" />
                  </svg>
                  <p class="text-sm text-slate-400">No se encontraron eventos con los filtros aplicados</p>
                  <button
                    @click="clearFilters"
                    class="text-xs text-emerald-400 hover:text-emerald-300 underline underline-offset-2 transition-colors"
                  >
                    Limpiar filtros
                  </button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <!-- Footer de la tabla -->
      <div class="px-6 py-3 border-t border-slate-700/50 bg-slate-800/30 flex items-center justify-between">
        <span class="text-xs text-slate-500">
          Mostrando {{ filteredEvents.length }} de {{ events.length }} eventos
        </span>
        <div class="flex items-center gap-1">
          <span class="text-[10px] text-slate-500 mr-2">Confianza OCR promedio:</span>
          <span class="text-xs font-semibold text-emerald-400">96.2%</span>
        </div>
      </div>
    </div>
  </div>
</template>
