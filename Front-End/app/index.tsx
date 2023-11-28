import React from "react";
import { StyleSheet, Text, View } from "react-native";
import { Link, useRouter } from "expo-router";
import Colors from "../constants/Colors";
import TextStyles from "../themedStyles/Text";
import Button from "../components/Button";
import Spacing from "../constants/Spacing";
import LottieView from "lottie-react-native";

type MenuItemProps = {
  href: string;
  title: string;
};

const Login: React.FC = () => {
  const router = useRouter();

  return (
    <View style={styles.container}>
      <View style={styles.textArea}>
        {/* <LottieView
          style={{ width: "80%" }}
          source={require("../assets/lottie/flashcards.json")}
          autoPlay
        /> */}
        <Text style={styles.title}>Bem Vindo!</Text>
        <Text style={styles.subtitle}>
          Venha aprender de forma rápida, divertida e eficiente com a gente.
        </Text>
      </View>
      <View style={styles.buttonArea}>
        <Button
          type="neutral"
          title="Criar Conta"
          onPress={() => router.push("/register")}
        />
        <Button
          type="primary"
          title="Entrar"
          onPress={() => router.push("/roomUsers")}
        />
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors["light"].primary,
  },
  title: {
    ...TextStyles.titleHeader,
    fontSize: 32,
  },
  subtitle: {
    ...TextStyles.titleHeader,
  },
  textArea: {
    flex: 1,
    justifyContent: "center",
    paddingHorizontal: Spacing.g,
  },
  text: {
    fontSize: 16,
  },
  link: {
    fontSize: 16,
    textDecorationLine: "underline",
  },
  separator: {
    marginVertical: 16,
    height: 1,
    width: "80%",
    backgroundColor: "black",
  },
  buttonArea: {
    backgroundColor: Colors["light"].white,
    paddingHorizontal: Spacing.g,
    paddingVertical: Spacing.xg,
    gap: Spacing.g,
    borderTopLeftRadius: Spacing.g,
    borderTopRightRadius: Spacing.g,
  },
});

export default Login;
