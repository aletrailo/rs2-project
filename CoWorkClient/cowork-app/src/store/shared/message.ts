import { notify } from "@kyvg/vue3-notification";

export function successMessage(message: string) {
    notify({
        title: " ",
        text: message,
        duration: 2000,
        type: 'success'
    });
}

export function errorMessage(message: string) {
    notify({
        title: "Greška!",
        text: message,
        duration: 2000,
        type: 'error'
    });
}