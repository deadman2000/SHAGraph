#-------------------------------------------------
#
# Project created by QtCreator 2018-04-22T10:13:19
#
#-------------------------------------------------

QT       += core gui opengl

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = Graphula
TEMPLATE = app

LIBS += -lopengl32

# The following define makes your compiler emit warnings if you use
# any feature of Qt which has been marked as deprecated (the exact warnings
# depend on your compiler). Please consult the documentation of the
# deprecated API in order to know how to port your code away from it.
DEFINES += QT_DEPRECATED_WARNINGS

# You can also make your code fail to compile if you use deprecated APIs.
# In order to do so, uncomment the following line.
# You can also select to disable deprecated APIs only up to a certain version of Qt.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0


SOURCES += \
    main.cpp \
    mainwindow.cpp \
    graphwidget.cpp \
    bitvalue.cpp \
    and.cpp \
    constantbit.cpp \
    or.cpp \
    xor.cpp \
    not.cpp \
    intvalue.cpp \
    bytevalue.cpp \
    sha.cpp \
    block256.cpp \
    camera.cpp

HEADERS += \
    mainwindow.h \
    graphwidget.h \
    bitvalue.h \
    and.h \
    constantbit.h \
    or.h \
    xor.h \
    not.h \
    intvalue.h \
    bytevalue.h \
    sha.h \
    block256.h \
    camera.h

INCLUDEPATH += ../libs

FORMS += \
        mainwindow.ui
