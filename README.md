# WPFFlowDemo
如果用ScrollViewer来进行滚动的话，会出现滚动到底部，然后直接跳到最开始，这样跳动的情况发生。
有时候我们需求的是无缝滚动，滚动到最底部，最顶部可以连着最底部接着滚动。
基于Canvas的TranslateTransform实现，并且结合WPF的  CompositionTarget.Rendering 事件实现无缝循环滚动。

基本原理是，如果一个button移出可见区域的时候，就把他放进等待队列里面，等到最后一个button即将全部出现的时候，从等待队列里面拿出一个button放在最后一个button下面。
