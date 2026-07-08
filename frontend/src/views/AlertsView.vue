<script setup>
/**
 * AlertsView.vue
 * Pantalla de alertas — Vista detallada de una placa en investigación.
 * Incluye análisis de patrón temporal, espacial, índice de riesgo,
 * y una línea de tiempo vertical con hitos de seguimiento.
 */
import { ref } from 'vue'

// Datos del vehículo bajo investigación
const vehicle = ref({
  plate: 'ABC-1234',
  brand: 'Toyota',
  color: 'Gris',
  type: 'Sedán',
  firstSeen: '2025-06-28 02:15:33',
  lastSeen: '2025-06-29 18:42:15',
  totalDetections: 17,
  riskLevel: 'ALTO'
})

// Bloques de análisis avanzado
const analysisBlocks = ref([
  {
    id: 'temporal',
    title: 'Patrón Temporal',
    subtitle: 'Análisis de frecuencia horaria',
    icon: 'clock',
    color: 'amber',
    riskTag: 'ANÓMALO',
    findings: [
      { label: 'Frecuencia nocturna', value: 'Alta', detail: '73% de detecciones entre 22:00-05:00' },
      { label: 'Horario predominante', value: '01:00 - 04:00', detail: 'Fuera del horario operativo normal' },
      { label: 'Días activos', value: '5 de 7', detail: 'Patrón recurrente de lunes a viernes' },
      { label: 'Intervalos', value: 'Irregulares', detail: 'Sin cadencia fija, sugiere vigilancia activa' }
    ]
  },
  {
    id: 'spatial',
    title: 'Patrón Espacial',
    subtitle: 'Análisis de ubicaciones frecuentes',
    icon: 'map',
    color: 'red',
    riskTag: 'NO AUTORIZADO',
    findings: [
      { label: 'Zona principal', value: 'Zona de Carga', detail: 'Sin autorización registrada en el sistema' },
      { label: 'Cobertura', value: '4 zonas', detail: 'Entrada Norte, Perímetro Este, Zona Carga, Perímetro Sur' },
      { label: 'Proximidad', value: 'Áreas restringidas', detail: 'Detecciones a <50m de zona de seguridad crítica' },
      { label: 'Movimiento', value: 'Perimetral', detail: 'Recorrido circular alrededor de la instalación' }
    ]
  },
  {
    id: 'risk',
    title: 'Índice de Riesgo',
    subtitle: 'Evaluación integral de amenaza',
    icon: 'gauge',
    color: 'red',
    riskTag: 'ALTO',
    findings: [
      { label: 'Nivel de riesgo', value: '87/100', detail: 'Calculado por modelo de evaluación de amenazas' },
      { label: 'Patrón de movimiento', value: 'Errático', detail: 'Cambios bruscos de dirección y velocidad variable' },
      { label: 'Coincidencia BBDD', value: 'Parcial', detail: 'Placa asociada a reporte previo sin resolución' },
      { label: 'Recomendación', value: 'Investigar', detail: 'Escalamiento a equipo de seguridad sugerido' }
    ]
  }
])

// Línea de tiempo de eventos
const timeline = ref([
  {
    id: 1,
    time: '18:42:15',
    date: '29 Jun 2025',
    title: 'Detección en Entrada Principal',
    description: 'Vehículo ingresa por acceso principal. OCR confirma placa con 98.5% de confianza.',
    zone: 'Entrada Principal',
    type: 'detection',
    status: 'latest'
  },
  {
    id: 2,
    time: '15:27:33',
    date: '29 Jun 2025',
    title: 'Avistamiento en Perímetro Este',
    description: 'Cámara PE-04 registra tránsito lento por zona perimetral sin parada autorizada.',
    zone: 'Perímetro Este',
    type: 'movement',
    status: 'warning'
  },
  {
    id: 3,
    time: '03:15:08',
    date: '29 Jun 2025',
    title: 'Detección Nocturna en Zona de Carga',
    description: 'Permanencia de 47 minutos en horario no operativo. Sin registro de entrega programada.',
    zone: 'Zona de Carga',
    type: 'alert',
    status: 'critical'
  },
  {
    id: 4,
    time: '01:48:22',
    date: '29 Jun 2025',
    title: 'Ingreso por Entrada Norte',
    description: 'Primera detección del día. Velocidad reducida al ingreso. Sin coincidencia con turnos activos.',
    zone: 'Entrada Norte',
    type: 'detection',
    status: 'warning'
  },
  {
    id: 5,
    time: '22:10:55',
    date: '28 Jun 2025',
    title: 'Rondín en Perímetro Sur',
    description: 'Vehículo detectado en 3 cámaras consecutivas del perímetro sur en un lapso de 12 minutos.',
    zone: 'Perímetro Sur',
    type: 'movement',
    status: 'warning'
  },
  {
    id: 6,
    time: '02:15:33',
    date: '28 Jun 2025',
    title: 'Primera Detección Registrada',
    description: 'Primer avistamiento del vehículo en el sistema. Placa incorporada a monitoreo automático.',
    zone: 'Entrada Norte',
    type: 'initial',
    status: 'info'
  }
])
</script>

<template>
  <div class="space-y-6">
    <!-- Header -->
    <div class="flex items-center justify-between flex-wrap gap-4">
      <div>
        <div class="flex items-center gap-3 mb-1">
          <h1 class="text-2xl font-bold text-slate-100">Alertas: Eventos de Investigación</h1>
          <span class="inline-flex items-center gap-1.5 px-3 py-1 rounded-full text-[11px] font-bold uppercase tracking-wider bg-red-950 text-red-400 border border-red-500/20 animate-pulse">
            <svg class="w-3 h-3" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="2">
              <path stroke-linecap="round" stroke-linejoin="round" d="M12 9v3.75m-9.303 3.376c-.866 1.5.217 3.374 1.948 3.374h14.71c1.73 0 2.813-1.874 1.948-3.374L13.949 3.378c-.866-1.5-3.032-1.5-3.898 0L2.697 16.126ZM12 15.75h.007v.008H12v-.008Z" />
            </svg>
            CASO ACTIVO
          </span>
        </div>
        <p class="text-sm text-slate-400">Vista detallada de vehículo bajo investigación</p>
      </div>
    </div>

    <!-- Ficha del Vehículo -->
    <div class="bg-slate-800/50 border border-slate-700/50 rounded-xl p-6 backdrop-blur-sm">
      <div class="flex flex-col lg:flex-row lg:items-center gap-6">
        <!-- Placa destacada -->
        <div class="flex items-center gap-5">
          <div class="relative">
            <div class="w-20 h-20 rounded-xl bg-slate-700/60 border-2 border-slate-600/40 flex items-center justify-center">
              <svg class="w-10 h-10 text-slate-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1">
                <path stroke-linecap="round" stroke-linejoin="round" d="M8.25 18.75a1.5 1.5 0 0 1-3 0m3 0a1.5 1.5 0 0 0-3 0m3 0h6m-9 0H3.375a1.125 1.125 0 0 1-1.125-1.125V14.25m17.25 4.5a1.5 1.5 0 0 1-3 0m3 0a1.5 1.5 0 0 0-3 0m3 0H6.375m11.25-4.5A1.125 1.125 0 0 0 16.5 7.875L14.625 3.75h-5.25L7.5 7.875A1.125 1.125 0 0 0 6.375 9v5.25m11.25-4.5V9M3.375 14.25h17.25" />
              </svg>
            </div>
            <!-- Indicador de riesgo -->
            <div class="absolute -top-1 -right-1 w-5 h-5 rounded-full bg-red-500 border-2 border-slate-800 flex items-center justify-center">
              <span class="text-[8px] font-bold text-white">!</span>
            </div>
          </div>
          <div>
            <div class="flex items-center gap-3">
              <span class="inline-flex items-center px-4 py-2 rounded-lg bg-slate-900 text-xl font-mono font-bold text-slate-100 border-2 border-red-500/30 tracking-widest">
                {{ vehicle.plate }}
              </span>
              <span class="inline-flex items-center gap-1.5 px-2.5 py-1 rounded-full text-[10px] font-bold uppercase tracking-wider bg-red-950 text-red-400 border border-red-500/20">
                RIESGO {{ vehicle.riskLevel }}
              </span>
            </div>
            <p class="text-sm text-slate-400 mt-2">{{ vehicle.brand }} {{ vehicle.color }} — {{ vehicle.type }}</p>
          </div>
        </div>

        <!-- Datos clave -->
        <div class="flex-1 grid grid-cols-2 sm:grid-cols-4 gap-4 lg:ml-auto">
          <div class="text-center p-3 rounded-lg bg-slate-900/50 border border-slate-700/30">
            <p class="text-[10px] text-slate-500 uppercase tracking-wider mb-1">Primera Detección</p>
            <p class="text-sm font-semibold text-slate-200">28 Jun</p>
            <p class="text-[10px] text-slate-400 font-mono">02:15:33</p>
          </div>
          <div class="text-center p-3 rounded-lg bg-slate-900/50 border border-slate-700/30">
            <p class="text-[10px] text-slate-500 uppercase tracking-wider mb-1">Última Detección</p>
            <p class="text-sm font-semibold text-slate-200">29 Jun</p>
            <p class="text-[10px] text-slate-400 font-mono">18:42:15</p>
          </div>
          <div class="text-center p-3 rounded-lg bg-slate-900/50 border border-slate-700/30">
            <p class="text-[10px] text-slate-500 uppercase tracking-wider mb-1">Total Detecciones</p>
            <p class="text-2xl font-bold text-amber-400">{{ vehicle.totalDetections }}</p>
          </div>
          <div class="text-center p-3 rounded-lg bg-red-950/30 border border-red-500/20">
            <p class="text-[10px] text-slate-500 uppercase tracking-wider mb-1">Nivel Riesgo</p>
            <p class="text-2xl font-bold text-red-400">87</p>
            <p class="text-[10px] text-red-400/70">/ 100</p>
          </div>
        </div>
      </div>
    </div>

    <!-- Bloques de Análisis Avanzado -->
    <div class="grid grid-cols-1 lg:grid-cols-3 gap-4">
      <div
        v-for="block in analysisBlocks"
        :key="block.id"
        :class="[
          'bg-slate-800/50 border rounded-xl p-5 backdrop-blur-sm transition-all duration-300 hover:shadow-lg',
          block.color === 'amber' ? 'border-amber-500/20 hover:border-amber-500/30 hover:shadow-amber-900/10' :
          'border-red-500/20 hover:border-red-500/30 hover:shadow-red-900/10'
        ]"
      >
        <!-- Header del bloque -->
        <div class="flex items-center justify-between mb-4">
          <div class="flex items-center gap-3">
            <div
              :class="[
                'w-9 h-9 rounded-lg flex items-center justify-center',
                block.color === 'amber' ? 'bg-amber-500/15' : 'bg-red-500/15'
              ]"
            >
              <!-- Clock -->
              <svg v-if="block.icon === 'clock'" :class="['w-5 h-5', block.color === 'amber' ? 'text-amber-400' : 'text-red-400']" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                <path stroke-linecap="round" stroke-linejoin="round" d="M12 6v6h4.5m4.5 0a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
              </svg>
              <!-- Map -->
              <svg v-if="block.icon === 'map'" class="w-5 h-5 text-red-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                <path stroke-linecap="round" stroke-linejoin="round" d="M9 6.75V15m0-8.25a1.5 1.5 0 0 1 3 0V15m0-8.25a1.5 1.5 0 0 1 3 0m-9 0V15m0-8.25A1.5 1.5 0 0 1 9 6.75m0 8.25V18a2.25 2.25 0 0 0 2.25 2.25h1.5A2.25 2.25 0 0 0 15 18v-2.25" />
              </svg>
              <!-- Gauge -->
              <svg v-if="block.icon === 'gauge'" class="w-5 h-5 text-red-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                <path stroke-linecap="round" stroke-linejoin="round" d="M3.75 3v11.25A2.25 2.25 0 0 0 6 16.5h2.25M3.75 3h-1.5m1.5 0h16.5m0 0h1.5m-1.5 0v11.25A2.25 2.25 0 0 1 18 16.5h-2.25m-7.5 0h7.5m-7.5 0-1 3m8.5-3 1 3m0 0 .5 1.5m-.5-1.5h-9.5m0 0-.5 1.5m.75-9 3-3 2.148 2.148A12.061 12.061 0 0 1 16.5 7.605" />
              </svg>
            </div>
            <div>
              <h3 class="text-sm font-semibold text-slate-100">{{ block.title }}</h3>
              <p class="text-[10px] text-slate-500">{{ block.subtitle }}</p>
            </div>
          </div>
          <span
            :class="[
              'px-2 py-0.5 rounded text-[9px] font-bold uppercase tracking-wider',
              block.color === 'amber' ? 'bg-amber-950 text-amber-400 border border-amber-500/20' :
              'bg-red-950 text-red-400 border border-red-500/20'
            ]"
          >
            {{ block.riskTag }}
          </span>
        </div>

        <!-- Hallazgos -->
        <div class="space-y-3">
          <div
            v-for="(finding, idx) in block.findings"
            :key="idx"
            class="p-3 rounded-lg bg-slate-900/40 border border-slate-700/20"
          >
            <div class="flex items-center justify-between mb-1">
              <span class="text-[10px] font-medium text-slate-400 uppercase tracking-wider">{{ finding.label }}</span>
              <span
                :class="[
                  'text-xs font-semibold',
                  block.color === 'amber' ? 'text-amber-400' : 'text-red-400'
                ]"
              >
                {{ finding.value }}
              </span>
            </div>
            <p class="text-[11px] text-slate-500 leading-relaxed">{{ finding.detail }}</p>
          </div>
        </div>
      </div>
    </div>

    <!-- Línea de Tiempo (Timeline) -->
    <div class="bg-slate-800/50 border border-slate-700/50 rounded-xl p-6 backdrop-blur-sm">
      <div class="flex items-center justify-between mb-6">
        <div>
          <h2 class="text-lg font-semibold text-slate-100">Línea de Tiempo de Seguimiento</h2>
          <p class="text-xs text-slate-400 mt-1">Historial cronológico de detecciones del vehículo {{ vehicle.plate }}</p>
        </div>
        <span class="text-xs text-slate-500">{{ timeline.length }} eventos registrados</span>
      </div>

      <!-- Timeline vertical -->
      <div class="relative">
        <!-- Línea vertical -->
        <div class="absolute left-[18px] top-0 bottom-0 w-px bg-gradient-to-b from-emerald-500/40 via-amber-500/30 to-slate-700/30"></div>

        <div class="space-y-1">
          <div
            v-for="(event, idx) in timeline"
            :key="event.id"
            class="relative pl-12 pb-6 group"
          >
            <!-- Punto en la línea -->
            <div
              :class="[
                'absolute left-2.5 top-1 w-[15px] h-[15px] rounded-full border-2 z-10 transition-transform duration-200 group-hover:scale-125',
                event.status === 'latest' ? 'bg-emerald-500 border-emerald-400 shadow-lg shadow-emerald-500/30' :
                event.status === 'critical' ? 'bg-red-500 border-red-400 shadow-lg shadow-red-500/30' :
                event.status === 'warning' ? 'bg-amber-500 border-amber-400' :
                'bg-slate-500 border-slate-400'
              ]"
            >
              <!-- Pulso para el evento más reciente -->
              <div
                v-if="event.status === 'latest'"
                class="absolute -inset-1 rounded-full bg-emerald-500/30 animate-ping"
              ></div>
            </div>

            <!-- Contenido del evento -->
            <div
              :class="[
                'p-4 rounded-lg border transition-all duration-200 hover:translate-x-1',
                event.status === 'critical'
                  ? 'bg-red-950/30 border-red-500/20 hover:border-red-500/40'
                  : event.status === 'warning'
                    ? 'bg-amber-950/15 border-amber-500/15 hover:border-amber-500/30'
                    : event.status === 'latest'
                      ? 'bg-emerald-950/20 border-emerald-500/20 hover:border-emerald-500/40'
                      : 'bg-slate-800/40 border-slate-700/30 hover:border-slate-600/40'
              ]"
            >
              <!-- Meta -->
              <div class="flex items-center gap-3 mb-2">
                <span class="text-xs font-mono font-semibold text-slate-300">{{ event.time }}</span>
                <span class="text-[10px] text-slate-500">{{ event.date }}</span>
                <span
                  :class="[
                    'ml-auto px-2 py-0.5 rounded text-[9px] font-semibold uppercase tracking-wider',
                    event.status === 'critical' ? 'bg-red-950 text-red-400' :
                    event.status === 'warning' ? 'bg-amber-950 text-amber-400' :
                    event.status === 'latest' ? 'bg-emerald-950 text-emerald-400' :
                    'bg-slate-700 text-slate-400'
                  ]"
                >
                  {{ event.zone }}
                </span>
              </div>

              <h4 class="text-sm font-semibold text-slate-100 mb-1">{{ event.title }}</h4>
              <p class="text-xs text-slate-400 leading-relaxed">{{ event.description }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
