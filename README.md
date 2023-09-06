# **CoWorkSpace – Web aplikacija za oglasavanje I iznajmljivanje kancelarijskog prostora**

## O aplikaciji

Aplikacija koja je namenjena za oglašavanje I iznajmljivanje kancelarijskog prostora.

Prilikom pristupanja aplikaciji otvara se portal za prijavljivanje korisnika.
Prijavljivanje se vrši tako što korisnik ukuca svoj username I password koji se zatim validiraju na servisu I ukoliko korisnik postoji on dobija pristup aplikaciji.

Ukoliko korisnik nema nalog, može ga besplatno kreirati klikom na KREIRAJ NALOG.

Prilikom kreiranja naloga unose se najosnovnije informacije o korisniku, korisničkom imenu I šifre.
Nakon prijavljivanja, na glavnoj stranici se prikazuju svi oglasi koji postoje u bazi podataka.

Prikaz oglasa sadrži sledeće informacije:
1. Naziv
2. Adresu
3. Cenu
4. Status da li je slobodan ili zauzet
5. Kao I sliku kancelarije

Otvaranjem pojedinacnog oglasa, otvara se ekran koji uz prikaz informacija ima mogucnost rezervisanja (u slučaju da je slobodan)
Korisnik nije ograničen samo na rezervisanje već može I da postavi svoj oglas.

Oglas se postavlja klikom na centralno dugme POSTAVI OGLAS.

Tom prilikom korisnik je dužan da unese sve navedene informacije kao obavezne.
Sa leve strane, postoji mogucnost pregleda ličnog profila.
Unutar profila, se vrši prikaz ličnih postavljenih oglasa kao I prikaz oglasa koji su rezervisani.
Lične oglase korisnik moze da edituje, u slučaju da ima potrebu za nekom promenom.

Aplikacija razlikuje dva tipa role po pitanju korisničkih naloga, Administrator I User.

Administrator rola se odnosi na celokupnu rolu unutar aplikacije (_i moze biti dodeljena samo od strane programera_) I administrator ima dodatnu mogućnost prikaza svih registrovanih naloga.

## Pokretanje aplikacije u razvojnom okruzenju:

Da bi se aplikacija pokrenula u razvojnom okruzenju potrebno je imati instalirane sledeće alate:
1.	Visual Studio 2022 (uz instaliran ASP.NET web development, aplikacija je razvijana na .NET 7)
2.	Docker desktop (latest, aplikacija je razvijana na v24.0.5 engine-u)
3.	Visual Studio Code
4.	NodeJS (Aplikacija je razvijana na v18.17.0) I odgovarajuci npm (Aplikacija je razvijana na v 9.6.7)

Kada se klonira repo, potrebno je prebaciti se (chekcout) na master granu.

### Pokretanje backend mikroservisa:

Otvoriti u Visual Studio klonirani repository I otvoriti coWorkSpace.sln.

_*Preporučeno je odraditi Clean + Rebuild solution._

Prebaciti startup na docker-compose I kliknuti start.

### Pokretanje frontend mikroservisa:
Otvoriti preko Visual Studio Code klonirani repostirory I upaliti Terminal.

U terminal pozvati naredbu npm install kako bi se preuzeli svi potrebni paketi. Ukoliko sve prodje kako treba, pozvati npm run serve kako bi se startovala aplikacija.

Ukoliko sve prodje kako treba, aplikacija osluškuje na http://localhost:8800
