<template>
<header style="background-color: yellow;">
    <nav style="display: flex; padding: 1rem; justify-content: space-between; align-items: center; ">
        <div style="display: flex;" @click="openSideMenu = true">
            <button type="button" style="display: inline-flex; padding: 0.625rem; margin: -0.625rem; justify-content: center; align-items: center; border-radius: 0.375rem; color: #374151; ">
                <i class="bi bi-list"></i>
                <!--i class="bi-alarm" style="font-size: 2rem; color: cornflowerblue;"></i-->
      
            </button>
        </div>
      <div class="display: flex; ">

            LOGO
            <span class="display: inline-block; font-weight: 600; color: #4B5563; ">CoWorkSpace</span>
        </div>
    </nav>
    
    <Transition duration="550" name="nested">
        <div v-if="openSideMenu" role="dialog" style="position: fixed; z-index: 50; top:0; right:0; bottom:0; left:0;">
            <div style="position: fixed; top: 0;right: 0;bottom: 0;left: 0; background-color: #6B7280; --bg-opacity: 0.75; transition-property: opacity; transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1); transition-duration: 300ms; " @click="openSideMenu = false" aria-hidden="true"></div>
            <div style="overflow-y: auto; position: fixed; top: 0; bottom: 0; z-index: 50; padding-top: 1.5rem; padding-bottom: 1.5rem; padding-left: 1.5rem; padding-right: 1.5rem; width: 16rem; background-color: #ffffff; ">
                <div style="display: flex; justify-content: space-between; align-items: center; ">
                    <a tabindex="-1">
                      LOGIN
                        <span>CoWorkSpace</span>
                    </a>
                    <button @click="openSideMenu = false" type="button" tabindex="-1" style="padding: 0.625rem; margin: -0.625rem; border-radius: 0.375rem; color: #374151; ">
                        <span style="position: absolute; width: 1px; height: 1px;padding: 0; margin: -1px; overflow: hidden; clip: rect(0, 0, 0, 0); white-space: nowrap; border-width: 0; ">Close menu</span>
                    </button>
                </div>
                <div style="display: flow-root; margin-top: 1.5rem; ">
                    <div style="border-top-width: 1px; ">
                        <div>
                            <a style="display: flex; padding-top: 0.5rem; padding-bottom: 0.5rem; padding-left: 0.75rem; padding-right: 0.75rem; align-items: center; border-radius: 0.375rem; font-size: 0.875rem; line-height: 1.25rem; font-weight: 500; color: #374151; :hover { color: #6366F1; }">
                    
                            </a>
                        </div>
                        <div>
                            <a style="display: flex; padding-top: 0.5rem; padding-bottom: 0.5rem; padding-left: 0.75rem; padding-right: 0.75rem; align-items: center; border-radius: 0.375rem; font-size: 0.875rem; line-height: 1.25rem; font-weight: 500; color: #374151; :hover {color: #6366F1;  }">

                            </a>
                        </div>
                        <div>
                            <a style="display: flex; padding-top: 0.5rem; padding-bottom: 0.5rem; padding-left: 0.75rem; padding-right: 0.75rem; align-items: center; border-radius: 0.375rem; font-size: 0.875rem; line-height: 1.25rem; font-weight: 500; color: #374151; :hover {color: #6366F1;  }">
                                
                            </a>
                        </div>
                        <div>
                            <a style="display: flex; padding-top: 0.5rem; padding-bottom: 0.5rem; padding-left: 0.75rem; padding-right: 0.75rem; align-items: center; border-radius: 0.375rem; font-size: 0.875rem; line-height: 1.25rem; font-weight: 500; color: #374151; :hover {color: #6366F1;  }">
                               
                            </a>
                        </div>
                        <div style="position: absolute; bottom: 0; width: 13rem;">
                            <a @click="logOut" style="display: flex; padding-top: 0.5rem; padding-bottom: 0.5rem; padding-left: 0.75rem; padding-right: 0.75rem; align-items: center; border-radius: 0.375rem; font-size: 0.875rem; line-height: 1.25rem; font-weight: 500; color: #374151; cursor: pointer; :hover {color: #6366F1;  }">
                                <ArrowRightOnRectangleIcon style="margin-right: 0.75rem; margin-left: -0.25rem; width: 1.5rem; height: 1.5rem; " />
                                <span class="truncate"> Odjavite se</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </Transition>

</header>
</template>

<script>
import {
    defineComponent
} from 'vue';

export default defineComponent({
    name: "MenuOptions",
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


.nested-enter-active .inner,
.nested-leave-active .inner {
    transition: all 0.5s ease-in-out;
}

.nested-enter-from .inner,
.nested-leave-to .inner {
    transform: translateX(-300px);
}
</style>
