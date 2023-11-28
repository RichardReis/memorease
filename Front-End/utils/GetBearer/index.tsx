import AsyncStorage from "@react-native-async-storage/async-storage";
import { AUTH_TOKEN } from "../../constants/Storage";

async function getBearer() {
  const token = await AsyncStorage.getItem(AUTH_TOKEN);

  if (token) {
    const userLogged = JSON.parse(token);
    const AuthStr = "Bearer ".concat(userLogged);
    const config = { headers: { Authorization: AuthStr } };
    return config;
  }

  return undefined;
}

export { getBearer };
