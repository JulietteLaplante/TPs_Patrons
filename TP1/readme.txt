+ Est-ce que le patron Singletons applique à votre système de tâches? 
Oui, le command executor est en charge de gérer la répartition des tâches sur un nombre de thread fixe. On veut donc une seule instance de ce command executor pour s'assurer que le nombre de threads est respecté. Cette instance doit donc être accessible à chaque fois qu'une commande doit être exécutée. Le Singleton permet donc de répondre à ces besoins.

+ Si oui, pouvez vous citer une solution qui n’utilisera pas ce dernier(bonus)?
Une autre solution serait d'utiliser l'injection de dépendances. Le principe étant de pouvoir donner une reference à la classe par des moyens autre que globaux. Notamment par paramètres.

+ Quel patron va-vous aider  à  construire des  flux  basés sur  un  protocole  tout en  gardant  toujours  la  même interface?
Le patron que nous pensons mettre en place pour abstraire les protocoles de communication utilisés  dans notre librairie est l'abstract factory. Cela permet de changer ou d'ajouter des protocoles de communication sans avoir besoin de modifier l'interface de communication. 

+ Quel patron utiliser pour composer les flux afin de créer des flux chiffrés et compressés?
Le patron que nous pensons mettre en place est celui du decorator. Il permet effectivement de pouvoir traiter les entrées et sorties de la communication, tout en combinant les traitements.

+ Est-ce que votre instance de protocole de communication devrait être un flux? Pourquoi?
Dans l'architecture que l'on a suivit, oui. L'instance de notre protocole de communication utilise toujours la même interface indépendamment du protocole utilisé. De ce fait en le considerant genériquement comme un flux cela permet de d'utiliser l'interface de communication directement, et au moyen d'un decorateur de chiffrer et/ou compresser ce flux a volonté.

+ Quel patron va-vous permettre de construire votre instance en permettant  de facilement ajouter des paramètres de construction plus  tard?
Un builder

