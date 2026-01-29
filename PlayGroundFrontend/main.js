const { createApp } = Vue;
const url = "https://playgroundlibcontroller20260110201910-fgf5a8baecere0bx.canadacentral-01.azurewebsites.net/PlayGround";

createApp({
    data() {
        return {
            playgrounds: [],
            // Objekt til den nye legeplads vi vil oprette
            formData: { name: "", maxChildren: 0, minAge: 0 } 
        }
    },
    async created() {
        this.getAll();
    },
    methods: {
        async getAll() {
            try {
                const response = await axios.get(url);
                this.playgrounds = response.data;
            } catch (ex) {
                alert(ex.message);
            }
        },
        async add() {
            try {
                // Sender data til Azure (POST)
                await axios.post(url, this.formData);
                
                // Opdaterer listen og nulstiller felterne
                this.getAll();
                this.formData = { name: "", maxChildren: 0, minAge: 0 };
            } catch (ex) {
                alert(ex.message);
            }
        },
        // OPGAVE PUNKT 3: Opdater (PUT)
        async updatePlayground() {
            try {
                // Vi skal bruge ID'et i URL'en (f.eks. .../PlayGround/5)
                const updateUrl = url + "/" + this.formData.id;
                
                await axios.put(updateUrl, this.formData);
                
                this.clearForm();
                this.getAll();
            } catch (ex) {
                alert(ex.message);
            }
        },
        // Hjælpefunktion: Når man trykker på "Rediger" i tabellen
        prepareEdit(pg) {
            // Vi skifter til "Edit mode"
            this.isEditing = true;
            // Vi kopierer data fra rækken op i formularen
            // {...pg} laver en kopi, så vi ikke retter direkte i tabellen før vi gemmer
            this.formData = { ...pg }; 
        },
        // Hjælpefunktion: Tømmer felterne og går tilbage til "Tilføj" mode
        clearForm() {
            this.isEditing = false;
            this.formData = { name: "", maxChildren: 0, minAge: 0 };
        }
    }
    
}).mount('#app');


// Brugeren skriver navn i inputfeltet -> Vue (v-model) opdaterer variablen formData i realtid.

// Brugeren trykker "Tilføj" -> Vue (@click) starter funktionen add().

// Axios tager dataene fra formData og sender dem til Azure (POST).

// Azure (C# Controlleren) gemmer dataene og siger "OK" tilbage.

// Axios fortæller Vue, at det lykkedes.

// Vue kalder getAll() igen -> Axios henter den nye liste -> Vue opdaterer automatisk tabellen (v-for) på skærmen.