1) Faites une architecture simple permettant de créer des capteurs qui auraient tous la même interface (Sensor). Quels patrons utiliseriez-vous ?
Une factory.

3) Faites une architecture simple permettant de construire une représentation en fonction d’une unité et d’un type. Quel patron utiliser ?
  Ici, nous pourrions utiliser une factory.
Mais nous avons décidé d'utiliser de la Reflection a la place. À partir de l'unité et du type, on parcourt les classes de notre projet qui possèdent un attribut que nous avons créé. Ainsi, on trouve automatiquement la classe Visualizer correspondante à notre type et notre unité. Si des unités et des types sont rajoutés, il n'y a rien à modifier contrairement à l'utilisation d'une factory. Il faut simplement créer les classes correspondantes.
  Il est a noté qu'étant donné le nombre incroyable d'unité différente, nous avons essayer d'abstraire l'unité de nos classes de Visualisation. Ainsi, nous n'avons qu'un TemperatureVisualizer et non pas un CelsiusTemperatureVisualizer et un FahrenheitTemperatureVisualizer. L'unité est donnée dans le constructeur de ces classes.


6) Créez une classe convertisseur qui sera à la fois un Sensor et un Visualizer.
  Ici, nous n'avons pas compris l'utilité d'avoir un convertisseur qui soit aussi un Visualizer.
Dans notre vision des choses, on a les Sensors d'un coté et les Visualizer de l'autre.
Parfois les sensors n'ont plus une unité adaptée alors on crée un convertisseur qui implémente l'interface Sensor. Ce Convertisseur encapsule le sensor et lorsque la méthode Sense() est appelée on appel la methode Sense() du Sensor, on converti cette valeur, puis on la renvoi.


CASTANO Nicolas
LAPLANTE Juliette
HAFFNER Yoann
