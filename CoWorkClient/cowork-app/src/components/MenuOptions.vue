<template>
<header style="background-color: yellow;">
    <nav style="display: flex; padding: 5px 20px; align-items: center; ">
        <div style="cursor: pointer;" @click="openSideMenu = true">
            <i class="bi bi-list" style="font-size: x-large; font-weight: bold;"></i>
        </div>
        <div>
            <router-link :to="{name : 'CoWorkHome'}" style="margin-left: 10px;"><span>CoWorkSpace</span>
            </router-link>
        </div>
    </nav>
    <Transition duration="550" name="nested">
        <div v-if="openSideMenu" role="dialog" style="position: fixed; z-index: 50; top:0; right:0; bottom:0; left:0;">
            <div style="position: fixed; top: 0;right: 0;bottom: 0;left: 0; background-color: #80808069; transition-property: opacity; transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1); transition-duration: 300ms; " @click="openSideMenu = false" aria-hidden="true"></div>
            <div style="overflow-y: auto; position: fixed; top: 0; bottom: 0; z-index: 50; padding-top: 1.5rem; padding-bottom: 1.5rem; padding-left: 1.5rem; padding-right: 1.5rem; width: 16rem; background-color: #ffffff; ">
                <div style="display: flex; justify-content: space-between; align-items: center;padding: 10px 0; border-bottom: 1px solid #8080804a; ">
                    <router-link :to="{name : 'CoWorkHome'}" tabindex="-1" style="cursor: pointer;">
                        <span>CoWorkSpace</span>
                    </router-link>
                    <button  @click="openSideMenu = false" type="button" class="btn-close" aria-label="Close"></button>
                </div>
                <div style="display: flow-root; margin-top: 1.5rem; ">
                    <div style="border-top-width: 1px; ">
                        <div class="item">
                            <router-link :to="{name: 'UserProfile'}"><i class="bi bi-person"></i>{{user.userName}} </router-link>
                        </div>
                        <div class="item">
                            <router-link :to="{name: 'UsersList'}">
                                <i class="bi bi-people"></i> Spisak korisnika
                            </router-link>
                        </div>
                        <div class="item" style="position: absolute; bottom: 0; width: 13rem;">
                            <a @click="logOut" style="cursor: pointer;">
                                <span > Odjavite se</span>
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
            openSideMenu: false
        }
    },
    methods: {
        logOut() {
            this.$store.dispatch('auth/logOut')
        },
    },
    computed: {
        user() {
            return this.$store.state.auth.user
        }
    }

});
</script>

<style scoped>
.item {
    padding: 10px;
}

.item:hover {
    color: indigo;
    background-color: #8080801c;
}


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
