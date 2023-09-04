<template>
<main style="margin-top: 50px;">
    <div class="main-wrapper">
        <picture class="scarecrow-img">
            <img src="https://raw.githubusercontent.com/nat-oku/devchallenges/main/Scarecrow.png" alt="scarecrow">
        </picture>
        <div class="error-text">
            <h3>404 Not Found</h3>
            <h2>Izgleda da ste malo zalutali</h2>
            <span class="input-group-btn">
                <router-link :to="{name: 'CoWorkHome'}"><button type="button" class="btn btn-warning">Vratite se na glavnu stranu</button></router-link>
            </span>
        </div>
    </div>

</main>
</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Poppins:wght@200;400&display=swap');

* {
    box-sizing: border-box;
    margin: 0;
    padding: 0;
}

html {
    font-size: 16px;
}

body {
    font-family: 'Poppins', sans-serif;
    height: 100vh;
    margin: 0;
    display: flex;
    flex-direction: column;
}

header {
    min-height: 50px;
}

.head-text {
    text-transform: uppercase;
    width: 200px;
    height: auto;
    padding: 20px;
}

.head-text,
.main-wrapper {
    width: 80%;
    margin: auto;
}

.main-wrapper {
    display: flex;
}

.scarecrow-img img {
    width: 90%;
    height: auto;
}

.error-text {
    width: 70%;
}

.error-text h2 {
    width: 80%;
    font-size: 3.75rem;
    letter-spacing: 0.5px;
    font-weight: normal;
}

.error-text p {
    width: 50%;
    padding: 5px;
}

button {
    margin-top: 30px;
    cursor: pointer;
    width: auto;
    padding: 15px;
 
}

footer {
    padding: 15px;
    text-align: center;
    min-height: 50px;
    margin-top: auto;
}

.fa-copyright {
    font-weight: lighter;
}

@media (max-width: 991.9px) {
    .main-wrapper {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }

    .error-text h2 {
        width: 100%;
        font-size: 3rem;
        line-height: 100%;
        padding-top: 15px;
    }

    .error-text p {
        width: 100%;
        font-size: 0.8rem;
        line-height: 150%;
        padding-top: 15px;
    }
}

@media (min-width: 992px) {
    .error-text h2 {
        width: 100%;
        line-height: 100%;
        padding-top: 15px;
    }

    .error-text p {
        width: 100%;
        line-height: 150%;
        padding-top: 15px;
    }
}
</style>
