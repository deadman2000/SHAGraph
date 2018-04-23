#include "repulsionforce.h"

#include <QDebug>
#include <QtMath>
#include "region.h"

class linRepulsion_antiCollision : public RepulsionForce
{
    double coefficient;
public:
    linRepulsion_antiCollision(double c) : coefficient(c) {}

    void apply(BitValue *n1, BitValue *n2)
    {
        // Get the distance
        double xDist = n1->x - n2->x;
        double yDist = n1->y - n2->y;
        double zDist = n1->z - n2->z;
        double distance = DIST3D(xDist, yDist, zDist) - NODE_SIZE*2;

        if (distance > 0) {
            // NB: factor = force / distance
            double factor = coefficient * n1->mass * n2->mass / distance / distance;

            n1->dx += xDist * factor;
            n1->dy += yDist * factor;
            n1->dz += zDist * factor;

            n2->dx -= xDist * factor;
            n2->dy -= yDist * factor;
            n2->dz -= zDist * factor;

        } else if (distance < 0) {
            double factor = 100 * coefficient * n1->mass * n2->mass;

            n1->dx += xDist * factor;
            n1->dy += yDist * factor;
            n1->dz += zDist * factor;

            n2->dx -= xDist * factor;
            n2->dy -= yDist * factor;
            n2->dz -= zDist * factor;
        }
    }

    void apply(BitValue *n, Region *r)
    {
        // Get the distance
        double xDist = n->x - r->massCenterX;
        double yDist = n->y - r->massCenterY;
        double zDist = n->z - r->massCenterZ;
        double distance = DIST3DSQ(xDist, yDist, zDist);

        if (distance > 0) {
            // NB: factor = force / distance
            double factor = coefficient * n->mass * r->mass / distance;

            n->dx += xDist * factor;
            n->dy += yDist * factor;
            n->dz += zDist * factor;
        }
    }

    void apply(BitValue *n, double g)
    {
        // Get the distance
        double xDist = n->x;
        double yDist = n->y;
        double zDist = n->z;
        double distance = DIST3D(xDist, yDist, zDist);

        if (distance > 0) {
            // NB: factor = force / distance
            double factor = coefficient * n->mass * g / distance;

            n->dx -= xDist * factor;
            n->dy -= yDist * factor;
            n->dz -= zDist * factor;
        }
    }
};

class linRepulsion : public RepulsionForce
{
    double coefficient;
public:
    linRepulsion(double c) : coefficient(c) {}

    void apply(BitValue *n1, BitValue *n2)
    {
        // Get the distance
        double xDist = n1->x - n2->x;
        double yDist = n1->y - n2->y;
        double zDist = n1->z - n2->z;
        double distance = DIST3DSQ(xDist, yDist, zDist);

        if (distance > 0) {
            // NB: factor = force / distance
            double factor = coefficient * n1->mass * n2->mass / distance;

            n1->dx += xDist * factor;
            n1->dy += yDist * factor;
            n1->dz += zDist * factor;

            n2->dx -= xDist * factor;
            n2->dy -= yDist * factor;
            n2->dz -= zDist * factor;
        }
    }

    void apply(BitValue *n, Region *r)
    {
        // Get the distance
        double xDist = n->x - r->massCenterX;
        double yDist = n->y - r->massCenterY;
        double zDist = n->z - r->massCenterZ;
        double distance = DIST3DSQ(xDist, yDist, zDist);

        if (distance > 0) {
            // NB: factor = force / distance
            double factor = coefficient * n->mass * r->mass / distance;

            n->dx += xDist * factor;
            n->dy += yDist * factor;
            n->dz += zDist * factor;
        }
    }

    void apply(BitValue *n, double g)
    {
        // Get the distance
        double xDist = n->x;
        double yDist = n->y;
        double zDist = n->z;
        double distance = DIST3D(xDist, yDist, zDist);

        if (distance > 0) {
            // NB: factor = force / distance
            double factor = coefficient * n->mass * g / distance;

            n->dx -= xDist * factor;
            n->dy -= yDist * factor;
            n->dz -= zDist * factor;
        }
    }
};

RepulsionForce *RepulsionForce::buildRepulsion(bool adjustBySize, double coefficient)
{
    if (adjustBySize) {
        return new linRepulsion_antiCollision(coefficient);
    } else {
        return new linRepulsion(coefficient);
    }
}

class strongGravity : public RepulsionForce
{
    double coefficient;
public:
    strongGravity(double c) : coefficient(c) {}

    void apply(BitValue *, BitValue *)
    {
    }

    void apply(BitValue *, Region *)
    {
    }

    void apply(BitValue *n, double g)
    {
        // Get the distance
        double xDist = n->x;
        double yDist = n->y;
        double zDist = n->z;
        double distance = DIST3DSQ(xDist, yDist, zDist);

        if (distance > 0) {
            // NB: factor = force / distance
            double factor = coefficient * n->mass * g;

            n->dx -= xDist * factor;
            n->dy -= yDist * factor;
            n->dz -= zDist * factor;
        }
    }
};

RepulsionForce *RepulsionForce::getStrongGravity(double coefficient)
{
    return new strongGravity(coefficient);
}
