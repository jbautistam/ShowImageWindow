Quizá lo más difícil de esta aplicación, haya sido buscarle un título a éste artículo. No sé muy bien cómo definirlo, en realidad es únicamente un experimento divertido.

El caso es que estaba viendo la presentación de [Scott Hanselman](https://www.hanselman.com/) de la [MSbuild](https://www.youtube.com/watch?v=EWYYgEkGJfs) 
(sí lo sé llevo un poco atrasadas las conferencias online) y sobre el minuto 26 aparece en la pantalla eSheep.

https://youtu.be/EWYYgEkGJfs?t=1593

[eSheep](https://github.com/Adrianotiger/desktopPet) es una aplicación que, como podéis ver, muestra el gráfico de una oveja caminando por la pantalla.

Este tipo de aplicaciones eran muy comunes hace unos años, teníamos gatos que perseguían al ratón, ovejas que subían por los laterales de las ventanas (y se
caían si las cerrabas), ojos que perseguían el cursor. No tenían ninguna utilidad, siguen sin tenerla, excepto ser divertidas.

Así que, como estaba buscando algo para entretenerme el fin de semana, hice mi propia versión con WPF. Como las ovejas ya estaban muy vistas, elegí a los Lemmings:

![Lemmings](https://twitter.com/i/status/1406227984357462024 "Lemmings")

Como véis la aplicación únicamente muestra un montón de Lemmings cayendo de la parte superior de la ventana, moviéndose por la barra de inicio de izquierda a derecha
y cavando de vez en cuando. Es aleatorio, no esperéis el juego completo.

Por supuesto, puedes seguir trabajando mientras los personajes evolucionan por la pantalla.

Pero ¿cómo se puede hacer ésto? Es fácil, en realidad todo son ventanas que se colocan por encima de todas las demás: no tienen fondo ni bordes, ni barra de títulos, sólo una imagen.

Recuerdo que hacer esto era muy complicado: había que utilizar GDI y controlar los comandos (WM_COMMAND) para que no dibujaran fondo. Era un lío de APIs de Windows bastante
complejo. Lo bueno es que con WPF se reduce al XAML de la ventana:

```XML
<Window x:Class="ShowImageWindow.Views.CharacterView"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ShowImageWindow.Views"
        mc:Ignorable="d" ShowInTaskbar="False"
        AllowsTransparency="True" WindowStyle="None" Background="Transparent" Topmost="True"
        Title="CharacterView" Height="450" Width="800" ResizeMode="NoResize">
	<Image x:Name="imgSprite" Stretch="None" MouseDown="imgSprite_MouseDown" MouseUp="imgSprite_MouseUp" />
</Window>
```

Esas propiedades de la ventana: `AllowTransparency`, `Background`, `Topmost` son las que hacen todo el trabajo. La imagen, es la que muestra el sprite adecuado.

Lo bueno es que seguimos teniendo control sobre el ratón en la imagen, por tanto, podemos pulsar sobre un Lemming que está cayendo y moverlo o sobre
un Lemming que está en el suelo y subirlo para que vuelva a caer. Y de nuevo, en WPF, el arrastrar una ventana aunque no tenga barra de título, es sorprendentemente
sencillo:

```csharp
private void imgSprite_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
{
	if (e.ClickCount == 2)
		GameObject.Engine.Destroy(GameObject);
	else
		DragMove();
}
```

La instrucción `DragMove` (que se ejecuta cuando se ha pulsado una vez con el ratón), hace toda la magia.

Por supuesto, no sería yo si no complicase las cosas un poco, no me bastaba con los Lemmings y acabé haciendo un motor que controlaba las animaciones o los
[spritesheets](https://en.wikipedia.org/wiki/Texture_atlas) de forma que simplemente añadiendo clases de control, pudiéramos mostrar otros gráficos. De hecho,
hay otra librería que muestra naves espaciales pero aún no está muy depurada y el movimiento no es demasiado bueno así que está comentado en la versión actual
(un día de éstos...).

Toda esta librería se encuentra en el proyecto `SpritesEngine`. Es un motor de juegos muy sencillo, no quería sobrepasar el límite del fin de semana así que lo
dejé en lo básico:

* Lectura de spritesheets: que sólo separa las imágenes de un archivo Atlas con un ancho y alto fijo, sin separaciones entre las diferentes imágenes. Por ejemplo,
el sprite sheet de los Lemmings cayendo lo podéis ver aquí:

**Nota:** por supuesto no lo dibujé yo, lo descargué de [Internet](http://www.spriters-resource.com/amiga_amiga_cd32/lemmings/sheet/37732/) después de buscar un poco en Google.

* Manejo de animaciones: asocia las imágenes a una animación.
* Modelos de personajes: todos los objetos que hay en el juego derivan de la clase `GameObject.cs` o de dos clases derivadas:
	* `VisualGameObject.cs`: es la clase base para los objetos que muestran sprites o gráficos. Básicamente, añade la posibilidad de animarlos y moverlos por la pantalla.
	* `BrainObject.cs:` es la clase base para los objetos del motor que no muestran gráficos. Normalmente se trata de objetos que levantan objetos de sprites o controlan
	colisiones (por ejemplo).

`GameObject` es una clase abstracta que proporciona dos métodos:

* `Initialize:` que se llama una vez cuando se "despierta" al objeto.
* `Update:` que se llama cada cierto tiempo para controlar los movimientos.

**Nota:** si queréis ver un motor de juegos bastante más complejo (e inacabado), podéis ver un [repositorio](https://github.com/jbautistam/CrioGame) 
de hace unos cuantos veranos.

Ese motor, en realidad sólo nos pone la base para nuestros personajes. Los Lemmings se encuentran en una librería aparte `CharactersLemmings` que contiene:

* `LemmingsManager.cs:` el controlador encargado de leer los gráficos y almacenar las animaciones.
* `LemmingsBrain.cs:` el objeto derivado de `BrainObject` encargado de mostrar un nuevo Lemming cada cierto tiempo (en este caso
no más de cinco al mismo tiempo),
* `LemmingsCharacter.cs:` el objeto derivado de `VisualGameObject` encargado de los movimientos.

Y eso es todo, como siempre, el código fuente lo tenéis en 
