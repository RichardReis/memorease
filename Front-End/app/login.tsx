import React from "react";
import { Dimensions, StyleSheet, Text, View } from "react-native";
import BuildForm from "../components/Form/BuildForm";
import { BuildInputsType } from "../components/Form/BuildInputs";
import { Link, Stack, useRouter } from "expo-router";
import Colors from "../constants/Colors";
import TextStyles from "../themedStyles/Text";
import Spacing from "../constants/Spacing";
import { GenericObjectType } from "../types/GenericObjectType";
import { AuthUser } from "../service/Account/login";

const Login: React.FC = () => {
  const router = useRouter();
  const inputs: BuildInputsType = [
    {
      name: "email",
      label: "E-mail",
      required: false,
      type: "text",
    },
    {
      name: "password",
      label: "Senha",
      required: false,
      type: "password",
    },
  ];

  const login = async (data: any) => {
    let response = await AuthUser(data);
    if (response) router.push("/(tabs)");
  };

  return (
    <View style={styles.container}>
      <Stack.Screen options={{ headerShown: false }} />
      <View style={styles.titleArea}>
        <Text style={styles.title}>Bem Vindo</Text>
        <Text style={styles.subtitle}>
          Informe seus dados abaixo para iniciar sua jornada de estudos!
        </Text>
      </View>
      <View style={styles.content}>
        <BuildForm buttonText="Logar" inputs={inputs} onSubmit={login} />
        <View style={styles.row}>
          <Text style={styles.text}>Primeira vez por aqui? </Text>
          <Link href={"/register"}>
            <Text style={styles.link}>Cadastre-se</Text>
          </Link>
        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    backgroundColor: Colors["light"].primary,
  },
  titleArea: {
    height: Dimensions.get("screen").height * 0.3,
    justifyContent: "flex-end",
    paddingHorizontal: Spacing.xg,
    paddingVertical: Spacing.s,
  },
  title: { ...TextStyles.titleHeader, fontSize: 32 },
  subtitle: { ...TextStyles.subTitleHeader, fontSize: 16 },
  content: {
    backgroundColor: Colors["light"].contentBackground,
    padding: Spacing.xg,
    flex: 1,
    borderTopRightRadius: Spacing.g,
    borderTopLeftRadius: Spacing.g,
  },
  text: { ...TextStyles.regular },
  link: {
    ...TextStyles.regular,
    ...TextStyles.link,
  },
  row: {
    marginTop: Spacing.g,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
  },
});

export default Login;
