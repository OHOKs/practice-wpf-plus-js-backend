
# Prisma Modell Készítése és Kapcsolatok

## Alap Prisma Modell Írása
A Prisma egy TypeScript-hez és Node.js-hez készült ORM, amely lehetővé teszi az adatbázis-modellek hatékony kezelését.

### 1. Alap Prisma Schema Fájl
```prisma
generator client {
  provider = "prisma-client-js"
}

datasource db {
  provider = "postgresql" // Az adatbázis típusa (pl. mysql, sqlite, postgresql)
  url      = env("DATABASE_URL") // Az adatbázis csatlakozási URL-je
}
```

### 2. Egyszerű Blog Alkalmazás Példa
#### Modellek
##### User Modell
```prisma
model User {
  id        Int      @id @default(autoincrement()) // Elsődleges kulcs
  name      String   // Felhasználó neve
  email     String   @unique // Az email egyedi
  posts     Post[]   // Kapcsolat a Post modellel (egy usernek több postja lehet)
  comments  Comment[] // Kapcsolat a Comment modellel (egy user több kommentet írhat)
  createdAt DateTime @default(now()) // Létrehozási dátum
}
```

##### Post Modell
```prisma
model Post {
  id        Int      @id @default(autoincrement())
  title     String
  content   String
  author    User     @relation(fields: [authorId], references: [id]) // Kapcsolat a User modellel
  authorId  Int      // Külső kulcs (foreign key)
  comments  Comment[] // Kapcsolat a Comment modellel
  createdAt DateTime @default(now())
}
```

##### Comment Modell
```prisma
model Comment {
  id        Int      @id @default(autoincrement())
  text      String
  post      Post     @relation(fields: [postId], references: [id]) // Kapcsolat a Post modellel
  postId    Int      // Külső kulcs
  user      User     @relation(fields: [userId], references: [id]) // Kapcsolat a User modellel
  userId    Int      // Külső kulcs
  createdAt DateTime @default(now())
}
```

---

## Kapcsolatok a Prisma-ban

### Egy-az-egyhez kapcsolat
#### Példa: User - Profile
```prisma
model User {
  id            Int             @id @default(autoincrement())
  profilePicture ProfilePicture? @relation(fields: [profilePictureId], references: [id])
  profilePictureId Int?
}

model ProfilePicture {
  id   Int  @id @default(autoincrement())
  url  String
  user User @relation(fields: [userId], references: [id])
  userId Int @unique
}
```

### Egy-a-többhöz kapcsolat
#### Példa: Author - Post
```prisma
model Author {
  id    Int    @id @default(autoincrement())
  name  String
  email String @unique
  posts Post[] // Egy szerző több cikket is írhat
}

model Post {
  id       Int     @id @default(autoincrement())
  title    String
  content  String
  author   Author  @relation(fields: [authorId], references: [id])
  authorId Int     // Külső kulcs az Author modellhez
}
```

### Több-a-többhöz kapcsolat
#### Példa: Post - Category
```prisma
model Post {
  id         Int         @id @default(autoincrement())
  title      String
  content    String
  categories Category[]  @relation("PostCategories")
}

model Category {
  id    Int    @id @default(autoincrement())
  name  String
  posts Post[] @relation("PostCategories")
}
```

---

## Prisma Client Használata

### Egy-az-egyhez kapcsolat lekérése
Lekérjük egy felhasználó profilját:
```typescript
const user = await prisma.user.findUnique({
  where: { id: 1 },
  include: { profile: true },
});
console.log(user);
```

### Egy-a-többhöz kapcsolat lekérése
Lekérjük egy szerző összes posztját:
```typescript
const author = await prisma.author.findUnique({
  where: { id: 1 },
  include: { posts: true },
});
console.log(author);
```

### Több-a-többhöz kapcsolat lekérése
Lekérjük egy cikk kategóriáit:
```typescript
const post = await prisma.post.findUnique({
  where: { id: 1 },
  include: { categories: true },
});
console.log(post);
```

Lekérjük egy kategória cikkjeit:
```typescript
const category = await prisma.category.findUnique({
  where: { id: 1 },
  include: { posts: true },
});
console.log(category);
```

---

## Összefoglaló
1. **Egy-az-egyhez**: Használj egyedi külső kulcsot az egyik modellen.  
2. **Egy-a-többhöz**: Az egyik modellben legyen lista típusú mező, a másikban külső kulcs.  
3. **Több-a-többhöz**: Egyszerűen definiálj lista típusú kapcsolatokat mindkét modellen, és a Prisma létrehozza a köztes táblát.  
4. **Lekérdezésnél** használd az `include` opciót a kapcsolt entitások betöltéséhez.
