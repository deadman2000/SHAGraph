del graph.png
set PATH=%PATH%;c:\Program Files (x86)\Graphviz2.38\bin\
dot -Tpng:cairo:gd -ograph.png graph.txt
@pause