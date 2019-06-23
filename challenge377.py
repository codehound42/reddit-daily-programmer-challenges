from scipy.optimize import linear_sum_assignment
from itertools import permutations
import numpy as np

def fit1(X, Y, x, y):
    return (X // x) * (Y // y)

def fit2(X, Y, x, y):
    return max(fit1(X, Y, x, y), fit1(X, Y, y, x))

def fit3Aux(X, Y, Z, x, y, z):
    return (X // x) * (Y // y) * (Z // z)

def fit3(X, Y, Z, x, y, z):
    best_result = 0
    for combination in list(permutations([x, y, z])):
        first = combination[0]
        second = combination[1]
        third = combination[2]
        result = fit3Aux(X, Y, Z, first, second, third)
        if result > best_result:
            best_result = result
    return best_result

def fitnAux(crate_dim, box_dim):
    n = len(box_dim)
    result = crate_dim[0] // box_dim[0]
    for i in range(1, n):
        result *= crate_dim[i] // box_dim[i]
    return result

def fitn(crate_dim, box_dim):
    n = len(box_dim)

    # Construct cost matrix
    cost_matrix = []
    for i in range(n):
        cost_array_i = []
        for j in range(n):
            costij = - np.log(crate_dim[i] // box_dim[j]) # Minimize sum of negative log values to get max product
            cost_array_i.append(costij)
        cost_matrix.append(np.asarray(cost_array_i))

    # Compute optimal box orientation using the Hungarian Algorithm
    (row_ind, col_ind) = linear_sum_assignment(np.asarray(cost_matrix))
    optimal_box_orientation = [box_dim[i] for i in col_ind]

    return fitnAux(crate_dim, optimal_box_orientation)

print("---fit1---")
print(fit1(25, 18, 6, 5)) # 12
print(fit1(10, 10, 1, 1)) # 100
print(fit1(12, 34, 5, 6)) # 10
print(fit1(12345, 678910, 1112, 1314)) # 5676
print(fit1(5, 100, 6, 1)) # 0

print("---fit2---")
print(fit2(25, 18, 6, 5)) # 15
print(fit2(12, 34, 5, 6)) # 12
print(fit2(12345, 678910, 1112, 1314)) # 5676
print(fit2(5, 5, 3, 2)) # 2
print(fit2(5, 100, 6, 1)) # 80
print(fit2(5, 5, 6, 1)) # 0

print("---fit3---")
print(fit3(10, 10, 10, 1, 1, 1)) # 1000
print(fit3(12, 34, 56, 7, 8, 9)) # 32
print(fit3(123, 456, 789, 10, 11, 12)) # 32604
print(fit3(1234567, 89101112, 13141516, 171819, 202122, 232425)) # 174648

print("---fitn---")
print(fitn([3, 4], [1, 2])) # 6
print(fitn([123, 456, 789], [10, 11, 12])) # 32604
print(fitn([123, 456, 789, 1011, 1213, 1415], [16, 17, 18, 19, 20, 21])) # 1883443968
print(fitn([180598, 125683, 146932, 158296, 171997, 204683, 193694, 216231, 177673, 169317, 216456, 220003, 165939, 205613, 152779, 177216, 128838, 126894, 210076, 148407], [1984, 2122, 1760, 2059, 1278, 2017, 1443, 2223, 2169, 1502, 1274, 1740, 1740, 1768, 1295, 1916, 2249, 2036, 1886, 2010])) # 4281855455197643306306491981973422080000
