#-------------------------------------------------
#
# Project created by QtCreator 2018-04-22T12:01:15
#
#-------------------------------------------------

QT       += testlib

QT       -= gui

TARGET = tst_testtest
CONFIG   += console
CONFIG   -= app_bundle

TEMPLATE = app

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
        tst_testtest.cpp \
        ../Graphula/Bits/bitvalue.cpp \
        ../Graphula/Bits/constantbit.cpp \
        ../Graphula/Bits/and.cpp \
        ../Graphula/Bits/or.cpp \
        ../Graphula/Bits/xor.cpp \
        ../Graphula/Bits/not.cpp \
        ../Graphula/Bits/intvalue.cpp \
        ../Graphula/Bits/bytevalue.cpp \
        ../Graphula/Bits/sha.cpp \
        ../Graphula/Bits/block256.cpp


INCLUDEPATH += ../Graphula/Bits/
