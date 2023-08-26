<template>
<header style="background-color: yellow;">
    <nav style="display: flex; padding: 1rem; justify-content: space-between; align-items: center; ">
        <div style="display: flex;" @click="openSideMenu = true">
            <button type="button" style="display: inline-flex; padding: 0.625rem; margin: -0.625rem; justify-content: center; align-items: center; border-radius: 0.375rem; color: #374151; ">
                <Bars3Icon style="width: 1.5rem; " />
            </button>
        </div>
        <div class="display: flex; ">

            LOGO
            <span class="display: inline-block; font-weight: 600; color: #4B5563; ">Kardioserb</span>
        </div>
    </nav>
    <!-- Mobile menu, show/hide based on menu open state. -->

    <!--Transition duration="550" name="nested">
        <div v-if="openSideMenu" role="dialog" class="fixed z-50 inset-0">
            <div class="outer fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity" @click="openSideMenu = false" aria-hidden="true"></div>
            <div class="inner fixed inset-y-0 z-50 w-64 overflow-y-auto bg-white px-6 py-6 sm:max-w-sm sm:ring-1 sm:ring-gray-900/10">
                <div class="flex items-center justify-between">
                    <a tabindex="-1">
                      LOGI
                        <span>Kardioserb</span>
                    </a>
                    <button @click="openSideMenu = false" type="button" tabindex="-1" class="-m-2.5 rounded-md p-2.5 text-gray-700">
                        <span class="sr-only">Close menu</span>
                        <XMarkIcon class="w-6" />
                    </button>
                </div>
                <div class="mt-6 flow-root">
                    <div class="divide-y divide-gray-500/10">
                        <div>
                            <router-link :to="{ name:'user', params: {id: loggedUser.id}}" class="text-gray-700 hover:text-indigo-500 group rounded-md px-3 py-2 flex items-center text-sm font-medium">
                                <UserIcon class="flex-shrink-0 -ml-1 mr-3 h-6 w-6" />
                                <span>{{ loggedUser.username }}</span>
                            </router-link>
                        </div>
                        <div>
                            <router-link :to="{name: 'search'}" class="text-gray-700 hover:text-indigo-500 group rounded-md px-3 py-2 flex items-center text-sm font-medium">
                                <UserGroupIcon class="flex-shrink-0 -ml-1 mr-3 h-6 w-6" />
                                <span class="truncate">Pretraga pacijenata</span>
                            </router-link>
                        </div>
                        <div>
                            <router-link v-if="loggedUser.role === 'admin'" :to="{name: 'users'}" class="text-gray-700 hover:text-indigo-500 group rounded-md px-3 py-2 flex items-center text-sm font-medium">
                                <UserGroupIcon class="flex-shrink-0 -ml-1 mr-3 h-6 w-6" />
                                <span class="truncate">Spisak korisnika</span>
                            </router-link>
                        </div>
                        <div>
                            <router-link v-if="loggedUser.role === 'admin'" :to="{name: 'reports'}" class="text-gray-700 hover:text-indigo-500 group rounded-md px-3 py-2 flex items-center text-sm font-medium">
                                <DocumentTextIcon class="flex-shrink-0 -ml-1 mr-3 h-6 w-6" />
                                <span class="truncate">Izveštaji</span>
                            </router-link>
                        </div>
                        <div class="absolute bottom-0 w-52">
                            <a @click="logOut" class="text-gray-700 hover:text-indigo-500 group rounded-md px-3 py-2 flex items-center text-sm font-medium cursor-pointer">
                                <ArrowRightOnRectangleIcon class="flex-shrink-0 -ml-1 mr-3 h-6 w-6" />
                                <span class="truncate"> Odjavite se</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </Transition-->

</header>
</template>

<script>
import {
    defineComponent
} from 'vue';
/*
import {
    Cog8ToothIcon,
    ArrowRightOnRectangleIcon,
    UserGroupIcon,
    UserIcon,
    DocumentTextIcon,
    Bars3Icon,
    XMarkIcon
} from '@heroicons/vue/24/outline'
*/
export default defineComponent({
    name: "MenuOptions",
    components: {
        /*  Cog8ToothIcon,
          ArrowRightOnRectangleIcon,
          UserGroupIcon,
          UserIcon,
          DocumentTextIcon,
          Bars3Icon,
          XMarkIcon*/
    },
    data() {
        return {
            openSideMenu: true
        }
    },
    methods: {
        logOut() {
            this.$store.dispatch('logOut')
        },
    },
    computed: {
        /*  loggedUser() {
              return this.$store.state.users.loggedUser
          },
          institutions() {
              return this.$store.state.institutions
          },
          selectedInstitution: {
              get() {
                  return this.$store.state.users.selectedInstitution
              },
              set(value) {
                  this.$store.state.users.selectedInstitution = value
              }
          },
          openSideMenu: {
              get() {
                  return this.$store.state.openSideMenu
              },
              set(value) {
                  this.$store.state.openSideMenu = value
              }
          }*/
    }

});
</script>

<style scoped>
.nested-enter-active,
.nested-leave-active {
    transition: all 0.5s ease-in-out;
}

.nested-enter-from,
.nested-leave-to {
    transform: none;
    opacity: 0;
}

/* we can also transition nested elements using nested selectors */
.nested-enter-active .inner,
.nested-leave-active .inner {
    transition: all 0.5s ease-in-out;
}

.nested-enter-from .inner,
.nested-leave-to .inner {
    transform: translateX(-300px);
}
</style>
