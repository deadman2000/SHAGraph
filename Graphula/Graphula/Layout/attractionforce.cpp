#include "attractionforce.h"

#include <QtMath>

class logAttraction_degreeDistributed_antiCollision : public AttractionForce
{
    double coefficient;
public:
    logAttraction_degreeDistributed_antiCollision(double c) : coefficient(c) {}

    void apply(BitValue * n1, BitValue * n2, double e) override
    {
        // Get the distance
        double xDist = n1->x - n2->x;
        double yDist = n1->y - n2->y;
        double zDist = n1->z - n2->z;
        double distance = DIST3D(xDist, yDist, zDist) - NODE_SIZE*2;

        if (distance > 0) {

            // NB: factor = force / distance
            double factor = -coefficient * e * log(1 + distance) / distance / n1->mass;

            n1->dx += xDist * factor;
            n1->dy += yDist * factor;
            n1->dz += zDist * factor;

            n2->dx -= xDist * factor;
            n2->dy -= yDist * factor;
            n2->dz -= zDist * factor;
        }
    }
};

class logAttraction_antiCollision : public AttractionForce
{
    double coefficient;
public:
    logAttraction_antiCollision(double c) : coefficient(c) {}

    void apply(BitValue * n1, BitValue * n2, double e) override
    {
        // Get the distance
        double xDist = n1->x - n2->x;
        double yDist = n1->y - n2->y;
        double zDist = n1->z - n2->z;
        double distance = DIST3D(xDist, yDist, zDist) - NODE_SIZE*2;

        if (distance > 0) {

            // NB: factor = force / distance
            double factor = -coefficient * e * log(1 + distance) / distance;

            n1->dx += xDist * factor;
            n1->dy += yDist * factor;
            n1->dz += zDist * factor;

            n2->dx -= xDist * factor;
            n2->dy -= yDist * factor;
            n2->dz -= zDist * factor;
        }
    }
};

class linAttraction_degreeDistributed_antiCollision : public AttractionForce
{
    double coefficient;
public:
    linAttraction_degreeDistributed_antiCollision(double c) : coefficient(c) {}

    void apply(BitValue * n1, BitValue * n2, double e) override
    {
        // Get the distance
        double xDist = n1->x - n2->x;
        double yDist = n1->y - n2->y;
        double zDist = n1->z - n2->z;
        double distance = DIST3D(xDist, yDist, zDist) - NODE_SIZE*2;

        if (distance > 0) {
            // NB: factor = force / distance
            double factor = -coefficient * e / n1->mass;

            n1->dx += xDist * factor;
            n1->dy += yDist * factor;
            n1->dz += zDist * factor;

            n2->dx -= xDist * factor;
            n2->dy -= yDist * factor;
            n2->dz -= zDist * factor;
        }
    }
};

class linAttraction_antiCollision : public AttractionForce
{
    double coefficient;
public:
    linAttraction_antiCollision(double c) : coefficient(c) {}

    void apply(BitValue * n1, BitValue * n2, double e) override
    {
        // Get the distance
        double xDist = n1->x - n2->x;
        double yDist = n1->y - n2->y;
        double zDist = n1->z - n2->z;
        double distance = DIST3D(xDist, yDist, zDist) - NODE_SIZE*2;

        if (distance > 0) {
            // NB: factor = force / distance
            double factor = -coefficient * e;

            n1->dx += xDist * factor;
            n1->dy += yDist * factor;
            n1->dz += zDist * factor;

            n2->dx -= xDist * factor;
            n2->dy -= yDist * factor;
            n2->dz -= zDist * factor;
        }
    }
};

class logAttraction_degreeDistributed : public AttractionForce
{
    double coefficient;
public:
    logAttraction_degreeDistributed(double c) : coefficient(c) {}

    void apply(BitValue * n1, BitValue * n2, double e) override
    {
        // Get the distance
        double xDist = n1->x - n2->x;
        double yDist = n1->y - n2->y;
        double zDist = n1->z - n2->z;
        double distance = DIST3D(xDist, yDist, zDist);

        if (distance > 0) {

            // NB: factor = force / distance
            double factor = -coefficient * e * log(1 + distance) / distance / n1->mass;

            n1->dx += xDist * factor;
            n1->dy += yDist * factor;
            n1->dz += zDist * factor;

            n2->dx -= xDist * factor;
            n2->dy -= yDist * factor;
            n2->dz -= zDist * factor;
        }
    }
};

class logAttraction : public AttractionForce
{
    double coefficient;
public:
    logAttraction(double c) : coefficient(c) {}

    void apply(BitValue * n1, BitValue * n2, double e) override
    {
        // Get the distance
        double xDist = n1->x - n2->x;
        double yDist = n1->y - n2->y;
        double zDist = n1->z - n2->z;
        double distance = DIST3D(xDist, yDist, zDist);

        if (distance > 0) {

            // NB: factor = force / distance
            double factor = -coefficient * e * log(1 + distance) / distance;

            n1->dx += xDist * factor;
            n1->dy += yDist * factor;
            n1->dz += zDist * factor;

            n2->dx -= xDist * factor;
            n2->dy -= yDist * factor;
            n2->dz -= zDist * factor;
        }
    }
};

class linAttraction_massDistributed : public AttractionForce
{
    double coefficient;
public:
    linAttraction_massDistributed(double c) : coefficient(c) {}

    void apply(BitValue * n1, BitValue * n2, double e) override
    {
        // Get the distance
        double xDist = n1->x - n2->x;
        double yDist = n1->y - n2->y;
        double zDist = n1->z - n2->z;

        // NB: factor = force / distance
        double factor = -coefficient * e / n1->mass;

        n1->dx += xDist * factor;
        n1->dy += yDist * factor;
        n1->dz += zDist * factor;

        n2->dx -= xDist * factor;
        n2->dy -= yDist * factor;
        n2->dz -= zDist * factor;
    }
};

class linAttraction : public AttractionForce
{
    double coefficient;
public:
    linAttraction(double c) : coefficient(c) {}

    void apply(BitValue * n1, BitValue * n2, double e) override
    {
        // Get the distance
        double xDist = n1->x - n2->x;
        double yDist = n1->y - n2->y;
        double zDist = n1->z - n2->z;

        // NB: factor = force / distance
        double factor = -coefficient * e;

        n1->dx += xDist * factor;
        n1->dy += yDist * factor;
        n1->dz += zDist * factor;

        n2->dx -= xDist * factor;
        n2->dy -= yDist * factor;
        n2->dz -= zDist * factor;
    }
};

AttractionForce *AttractionForce::buildAttraction(bool isLogAttraction, bool distributedAttraction, bool adjustBySize, double coefficient)
{
    if (adjustBySize) {
        if (isLogAttraction) {
            if (distributedAttraction) {
                return new logAttraction_degreeDistributed_antiCollision(coefficient);
            } else {
                return new logAttraction_antiCollision(coefficient);
            }
        } else {
            if (distributedAttraction) {
                return new linAttraction_degreeDistributed_antiCollision(coefficient);
            } else {
                return new linAttraction_antiCollision(coefficient);
            }
        }
    } else {
        if (isLogAttraction) {
            if (distributedAttraction) {
                return new logAttraction_degreeDistributed(coefficient);
            } else {
                return new logAttraction(coefficient);
            }
        } else {
            if (distributedAttraction) {
                return new linAttraction_massDistributed(coefficient);
            } else {
                return new linAttraction(coefficient);
            }
        }
    }
}
