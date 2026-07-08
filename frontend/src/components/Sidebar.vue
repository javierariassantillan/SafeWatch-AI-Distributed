<script setup>
    const props = defineProps({
        currentView: {
            type: String,
            default: 'dashboard'
        }
    })

    const emit = defineEmits(['navigate'])

    const navItems = [
        { id: 'dashboard', label: 'Dashboard', icon: 'grid' },
        { id: 'events', label: 'Eventos', icon: 'list' },
        { id: 'alerts', label: 'Alertas', icon: 'bell' },
        { id: 'settings', label: 'Análisis de Video', icon: 'camera' }
    ]

    function handleNavigate(viewId) {
        emit('navigate', viewId)
    }
</script>

<template>
    <aside class="w-64 min-h-screen bg-slate-950 border-r border-slate-800 flex flex-col">
        <div class="px-6 py-6 border-b border-slate-800">
            <div class="flex items-center gap-3">
                <div class="w-10 h-10 rounded-lg bg-emerald-600/20 flex items-center justify-center flex-shrink-0">
                    <svg class="w-6 h-6 text-emerald-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                        <path stroke-linecap="round" stroke-linejoin="round"
                              d="M9 12.75 11.25 15 15 9.75m-3-7.036A11.959 11.959 0 0 1 3.598 6 11.99 11.99 0 0 0 3 9.749c0 5.592 3.824 10.29 9 11.623 5.176-1.332 9-6.03 9-11.622 0-1.31-.21-2.571-.598-3.751h-.152c-3.196 0-6.1-1.248-8.25-3.285Z" />
                    </svg>
                </div>

                <div>
                    <h1 class="text-lg font-bold text-slate-100 tracking-tight">SafeWatch</h1>
                    <span class="text-xs font-semibold text-emerald-400 tracking-widest uppercase">AI</span>
                </div>
            </div>
        </div>

        <nav class="flex-1 px-3 py-4 space-y-1">
            <button v-for="item in navItems"
                    :key="item.id"
                    @click="handleNavigate(item.id)"
                    :class="[
          'w-full flex items-center gap-3 px-4 py-3 rounded-lg text-sm font-medium transition-all duration-200',
          currentView === item.id
            ? 'bg-emerald-600/15 text-emerald-400 shadow-lg shadow-emerald-900/20'
            : 'text-slate-400 hover:bg-slate-800/60 hover:text-slate-200'
        ]">
                <svg v-if="item.icon === 'grid'" class="w-5 h-5 flex-shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                    <path stroke-linecap="round" stroke-linejoin="round"
                          d="M3.75 6A2.25 2.25 0 0 1 6 3.75h2.25A2.25 2.25 0 0 1 10.5 6v2.25a2.25 2.25 0 0 1-2.25 2.25H6a2.25 2.25 0 0 1-2.25-2.25V6ZM3.75 15.75A2.25 2.25 0 0 1 6 13.5h2.25a2.25 2.25 0 0 1 2.25 2.25V18a2.25 2.25 0 0 1-2.25 2.25H6A2.25 2.25 0 0 1 3.75 18v-2.25ZM13.5 6a2.25 2.25 0 0 1 2.25-2.25H18A2.25 2.25 0 0 1 20.25 6v2.25A2.25 2.25 0 0 1 18 10.5h-2.25a2.25 2.25 0 0 1-2.25-2.25V6ZM13.5 15.75a2.25 2.25 0 0 1 2.25-2.25H18a2.25 2.25 0 0 1 2.25 2.25V18A2.25 2.25 0 0 1 18 20.25h-2.25a2.25 2.25 0 0 1-2.25-2.25v-2.25Z" />
                </svg>

                <svg v-if="item.icon === 'list'" class="w-5 h-5 flex-shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                    <path stroke-linecap="round" stroke-linejoin="round"
                          d="M8.25 6.75h12M8.25 12h12m-12 5.25h12M3.75 6.75h.007v.008H3.75V6.75Zm.375 0a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0ZM3.75 12h.007v.008H3.75V12Zm.375 0a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Zm-.375 5.25h.007v.008H3.75v-.008Zm.375 0a.375.375 0 1 1-.75 0 .375.375 0 0 1 .75 0Z" />
                </svg>

                <svg v-if="item.icon === 'bell'" class="w-5 h-5 flex-shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                    <path stroke-linecap="round" stroke-linejoin="round"
                          d="M14.857 17.082a23.848 23.848 0 0 0 5.454-1.31A8.967 8.967 0 0 1 18 9.75V9A6 6 0 0 0 6 9v.75a8.967 8.967 0 0 1-2.312 6.022c1.733.64 3.56 1.085 5.455 1.31m5.714 0a24.255 24.255 0 0 1-5.714 0m5.714 0a3 3 0 1 1-5.714 0" />
                </svg>

                <svg v-if="item.icon === 'camera'" class="w-5 h-5 flex-shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24" stroke-width="1.5">
                    <path stroke-linecap="round" stroke-linejoin="round"
                          d="M15.75 10.5 20.25 7.875A.75.75 0 0 1 21.375 8.52v6.96a.75.75 0 0 1-1.125.645L15.75 13.5m-12-6h10.5A1.5 1.5 0 0 1 15.75 9v6A1.5 1.5 0 0 1 14.25 16.5H3.75A1.5 1.5 0 0 1 2.25 15V9A1.5 1.5 0 0 1 3.75 7.5Z" />
                </svg>

                <span>{{ item.label }}</span>

                <span v-if="item.id === 'alerts'"
                      class="ml-auto inline-flex items-center justify-center w-5 h-5 text-[10px] font-bold rounded-full bg-red-500/20 text-red-400">
                    3
                </span>
            </button>
        </nav>

        <div class="px-4 py-4 border-t border-slate-800">
            <div class="flex items-center gap-3 px-2">
                <div class="w-8 h-8 rounded-full bg-slate-700 flex items-center justify-center">
                    <span class="text-xs font-semibold text-slate-300">OP</span>
                </div>

                <div class="flex-1 min-w-0">
                    <p class="text-sm font-medium text-slate-200 truncate">Operador</p>
                    <p class="text-xs text-slate-500 truncate">Centro de Monitoreo</p>
                </div>

                <div class="w-2 h-2 rounded-full bg-emerald-400 animate-pulse"></div>
            </div>
        </div>
    </aside>
</template>