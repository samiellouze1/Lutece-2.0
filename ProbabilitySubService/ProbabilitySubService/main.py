from typing import List, Dict
from fastapi import FastAPI
import numpy as np

app = FastAPI()

@app.post("/calculate_probabilities")
async def calculate_probabilities(historical_prices: List[float]):
    mean = np.mean(historical_prices)
    std_dev = np.std(historical_prices)
    
    possible_prices = np.linspace(mean - 3 * std_dev, mean + 3 * std_dev, 100)
    probability_distribution = (1 / (std_dev * np.sqrt(2 * np.pi))) * \
                              np.exp(-0.5 * ((possible_prices - mean) / std_dev)**2)
    
    returndict = dict(zip(possible_prices, probability_distribution))
    return returndict
