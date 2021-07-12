# UI

## Webpack

## Styles
A good read about sass architecture/folder structure: https://sass-guidelin.es/#architecture


###Modified BEM class convention
The BEM naming convention, whilst very good at promoting limited nesting, does have some issues. I find it to 
be quite clunky when you start adding modifiers or even multiple classes to a single element. I suggest 
separating modifiers from the blocks/elements they modify for shorter code and better readability.

```
// Block/Elemment/Modifier B/E/M
.block{
    &__element{
        &--modifier{}
        &--modifier2{}
    }
}

<div class="block">
    <div class="block__element"></div>
    <div class="block__element block__element--modifier"></div>
    <div class="block__element block__element--modifier block__element--modifier"></div>
</div>

// Modified BEM
.block{
    &__element{
        .-modifier{}
        .-modifier2{}
    }
}

<div class="block">
    <div class="block__element"></div>
    <div class="block__element -modifier"></div>
    <div class="block__element -modifier -modifier2"></div>
</div>
```
My suggested approach is similar to the concepts described in this article: https://css-tricks.com/abem-useful-adaptation-bem/

### An example solution structure
Based on the 7-1 Pattern: https://gist.github.com/rveitch/84cea9650092119527bc
```
sass
|    main.scss
|    _shame.scss
|
|--- variables
|
|--- base
|
|--- components
|
|--- layout
|
|--- pages
|
|--- vendor
|

```
### _shame.scss
https://sass-guidelin.es/#shame-file

- This scss file contains all the CSS declarations, hacks and things we are not proud of.
- This should be the only file in the codebase that includes the !important tag.

## Scripts

## Static resources 