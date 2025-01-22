# LazaMVVM

## About
LazaMVVM allows you to bind data directly to UI in Unity

## Hello world using a view model
1. Create a new TextMeshPro (Text)
2. Create a new model view script and call it HelloWorldModelView
<br/>2.1 Inherit it from MonoBehaviourViewModel
<br/>2.2 Create a new ViewModelField called HelloWorldString and initialize it
<br/>2.3 On Awake change this string to "Hello World"
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/HelloWorld/hello_1.png?raw=true"/>
3. Add this new HelloWorldModelView to the previously created canvas like this
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/HelloWorld/hello_2.png?raw=true"/>
4. Add TextMeshProFiewldBind to the TextMeshPro (Text) previously created like this
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/HelloWorld/hello_3.png?raw=true"/>
5. Bind your view model field (string) to unity TextMeshPro using the TextMeshProFieldBind and select HelloWorldString
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/HelloWorld/hello_4.png?raw=true"/>
6. Save the scene and run it, the result should be like this
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/HelloWorld/hello_5.png?raw=true"/>

## Advanced topics
<details>
<summary>How to create a new bind</summary>
1. Create a new script and inherit from BaseFieldBind, the type depends of what you want, for example if it's a Image can be a Sprite but in the text type can be a object and be parsed as string
<br/>2. Define the BindFilter, this will filter for you everytype that will be allowed to use for this bind
<br/>3. Define the OnValueChanged according to the type previously added, check others bind to use as reference if needed
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/new_bind.png?raw=true"/>
</details>

<details>
<summary>How to create and use a list bind</summary>
1. Create a new scroll view
<br/><img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/ListBind/listbind_1.png?raw=true"/>
<br/>2. Create a new TextMeshPro (Text) to use as a prefab
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/ListBind/listbind_2.png?raw=true"/>
<br/>3. Add new ListBind Script to the ScrollView container
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/ListBind/listbind_3.png?raw=true"/>
<br/>4. Create a new HelloWorldItem
<br/>4.1 Inherit from BaseViewModelItem
<br/>4.2 Don't forget to add the [ListItem] attribute to it
<br/>4.3 Add a new ViewModelField<string> to it and change it on constructor like the image below
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/ListBind/listbind_4.png?raw=true"/>
<br/>5. Create a new HelloWorldModelView simillar to "Hello World using a view model"
<br/>5.1 Add a new ViewModelListField<HelloWorldItem> and initialize it
<br/>5.2 Instantiate a new one each time it A is pressed on keyboard like the image below
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/ListBind/listbind_5.png?raw=true"/>
<br/>6. Create and open the item Prefab
<br/>6.1 Add the ViewModelProvider and select the HelloWorldItem (this list will show all the itens that has ListItem attribute)
<br/>6.2 Add TextMeshProFiewldBind script and use the ViewModelProvider to properly set the HelloWorldString
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/ListBind/listbind_6.png?raw=true"/>
<br/>7. Set the correct values to ListBind
<br/>7.1 The view Model should be the HelloWorldModelView
<br/>7.2 Select the correct ViewModelListField (It will be filtered automatically)
<br/>7.3 The template should be the prefab previously created (Item prefab)
<br/>7.4 The parent is where the prefab should be instantiated, in our case it should be the Content Rect transform
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/ListBind/listbind_7.png?raw=true"/>
<br/>8. If everything is correctly after playing each time you press "A" a new item will be instantiated
<img src="https://github.com/rsdal/LazaMVVM/blob/develop/docs/ListBind/listbind_8.png?raw=true"/>
</details>

## How to install

- open Package Manager
- click +
- select Install Package from Git URL
- paste https://github.com/rsdal/LazaMVVM.git
- click in Install

