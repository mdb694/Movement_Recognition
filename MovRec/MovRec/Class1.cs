using System;

public class Class1
{

    double[,,] samp = new double[5, 200, 9];

    double[,,] modulo(double[,,] d)
    {
        double [,,] mod = new double[5, 200, 3];

        for(int i=0; i<5; i++)
        {
            for(int j=0; j<200; j++)
            {
                for(int k=0; k<9; k=k+3)
                {
                    mod[i, j, k] = sqrt( pow(d[i,j,k], 2) + pow(d[i, j, k+1], 2) + pow(d[i, j, k+2], 2) );
                }
                
            }
        }

        return mod;
    }
}