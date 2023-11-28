import AsyncStorage from "@react-native-async-storage/async-storage";

//Services
import { api } from "./axios";

//Constants
import { AUTH_TOKEN } from "../constants/Storage";

//Utils
import { getBearer } from "../utils/GetBearer";

//Types
import { LoginType } from "../types/LoginType";
import { LoginResult } from "../types/LoginResult";
import { LoginResponse } from "../types/LoginResponse";
import { APIResponse } from "../types/APIResponse";

///--------------------------------------------------------------------///
///----------------- Função de login no sistema ------------------------///
///--------------------------------------------------------------------///
async function signIn(Data: LoginType) {
  const response: LoginResult = { Success: false };
  try {
    await api
      .post("/Account/Login", {
        email: Data.email,
        password: Data.password,
      })
      .then((result) => {
        const responseAPI = result.data as LoginResponse;

        if (responseAPI?.token !== null) {
          api.defaults.headers.common[
            "Authorization"
          ] = `Bearer ${responseAPI.token}`;

          AsyncStorage.setItem(AUTH_TOKEN, JSON.stringify(responseAPI.token));
          response.Success = true;
        }
      })
      .catch(function (error) {
        if (error.response) {
          switch (Number(error.response.status)) {
            case 401:
              singOut();
              break;
            case 404:
              response.Success = false;
              response.Message = "Usuário e/ou senha inválidos";
              break;
            case 500:
              response.Success = false;
              response.Message =
                "Ops, ocorreu uma falha em nosso sistema. Desculpe-nos pelo transtorno.";
              break;
          }
        } else {
          response.Success = false;
          response.Message =
            "Ops, ocorreu uma falha em nosso sistema. Desculpe-nos pelo transtorno.";
        }
      });

    return response;
  } catch {
    response.Success = false;
    response.Message =
      "Ops, ocorreu uma falha em nosso sistema. Desculpe-nos pelo transtorno.";
    return response;
  }
}

///--------------------------------------------------------------------///
///----------------- Função de logout no sistema ------------------------///
///--------------------------------------------------------------------///
async function singOut() {
  await AsyncStorage.removeItem(AUTH_TOKEN);
  let keys = await AsyncStorage.getAllKeys();
  keys.map((item) => {
    AsyncStorage.removeItem(item);
  });
  api.defaults.headers.common["Authorization"] = ``;
}

///--------------------------------------------------------------------///
///----------------- Função de requisição GET -------------------------///
///--------------------------------------------------------------------///
async function GetRequest<T>(path: string) {
  let response: APIResponse<T> = { success: false, object: {} as T };
  const config = await getBearer();
  try {
    await api
      .get(path, config)
      .then((result) => {
        response.success = result.data.success as boolean;
        response.number = result.data.number as number;
        response.object = result.data.object as T;
        response.message = result.data.message as string;
      })
      .catch(async function (error) {
        if (error.response) {
          switch (error.response.status) {
            case 400:
              response.message = error.response.data.message;
            case 401:
              singOut();
              break;
            case 500:
              response.message = error.response.data.message;
          }
        } else {
          response.message =
            "Ops, ocorreu uma falha em nosso sistema. Desculpe-nos pelo transtorno.";
        }
      });
    return response;
  } catch {
    response.success = false;
    response.message =
      "Ops, ocorreu uma falha em nosso sistema. Desculpe-nos pelo transtorno.";
    return response;
  }
}

///--------------------------------------------------------------------///
///----------------- Função de requisição POST -------------------------///
///--------------------------------------------------------------------///
async function PostRequest<T>(path: string, data: {}) {
  let response: APIResponse<T> = { success: false, object: {} as T };
  const config = await getBearer();
  try {
    await api
      .post(path, data, config)
      .then((result) => {
        response.success = result.data.success;
        response.number = result.data.number;
        response.object = result.data.object as T;
        response.message = result.data.message;
      })
      .catch(async function (error) {
        if (error.response) {
          switch (error.response.status) {
            case 400:
              response.success = false;
              response.message = error.response.data.message;
            case 401:
              singOut();
              break;
            case 500:
              response.success = false;
              response.message = error.response.data.message;
          }
        } else {
          response.success = false;
          response.message =
            "Ops, ocorreu uma falha em nosso sistema. Desculpe-nos pelo transtorno.";
        }
      });
    return response;
  } catch {
    response.success = false;
    response.message =
      "Ops, ocorreu uma falha em nosso sistema. Desculpe-nos pelo transtorno.";
    return response;
  }
}

///--------------------------------------------------------------------///
///----------------- Função de requisição DELETE -------------------------///
///--------------------------------------------------------------------///
async function DeleteRequest<T>(path: string) {
  let response: APIResponse<T> = { success: false, object: {} as T };
  const config = await getBearer();
  try {
    await api
      .delete(path, config)
      .then((result) => {
        response.success = result.data.success;
        response.number = result.data.number;
        response.object = result.data.object as T;
        response.message = result.data.message;
      })
      .catch(async function (error) {
        if (error.response) {
          switch (error.response.status) {
            case 400:
              response.success = false;
              response.message = error.response.data.message;
            case 401:
              singOut();
              break;
            case 500:
              response.success = false;
              response.message = error.response.data.message;
          }
        } else {
          response.success = false;
          response.message =
            "Ops, ocorreu uma falha em nosso sistema. Desculpe-nos pelo transtorno.";
        }
      });
    return response;
  } catch {
    response.success = false;
    response.message =
      "Ops, ocorreu uma falha em nosso sistema. Desculpe-nos pelo transtorno.";
    return response;
  }
}

export { GetRequest, PostRequest, DeleteRequest, signIn, singOut };
